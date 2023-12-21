import agent from "../../app/api/agent";
import { createAsyncThunk, createEntityAdapter, createSlice } from "@reduxjs/toolkit";

const incomesAdapter = createEntityAdapter();

function getAxiosParams(pagingParams) {
    const params = new URLSearchParams();
    params.append('pageNumber', pagingParams.pageNumber.toString());
    params.append('pageSize', pagingParams.pageSize.toString());
    params.append('startDate', pagingParams.startDate);
    params.append('endDate', pagingParams.endDate);
    return params;
}

// export const fetchIncomesAsync = createAsyncThunk('catalog/fetchIncomesAsync', async (_, thunkAPI) => {
//     const params = getAxiosParams(thunkAPI.getState().catalog.productParams)
//     try {
//         var response = await agent.Catalog.list(params);
//         thunkAPI.dispatch(setMetaData(response.metaData));
//         return response.items;
//     } catch (error) {
//         return thunkAPI.rejectWithValue({ error: error.data });
//     }
// });

// export const fetchIncomeAsync = createAsyncThunk('catalog/fetchIncomeAsync', async (productId, thunkAPI) => {
//     try {
//         const product = await agent.Catalog.details(productId);
//         return product;
//     } catch (error) {
//         return thunkAPI.rejectWithValue({ error: error.data });
//     }
// });

export const fetchIncomesAsync = createAsyncThunk(
    'incomes', async (_, thunkAPI) => {
        const params = getAxiosParams(thunkAPI.getState().incomes.pagingParams);
        try {
            var response = await agent.Incomes.list(params);
            //thunkAPI.dispatch(setMetaData(response.metaData));
            return response.items;
        } catch (error) {
            return thunkAPI.rejectWithValue({ error: error.data });
        }
        
    }
)

function initParams() {
    return {
        pageNumber: 1,
        pageSize: 25,
        //ToDo: later read about datetime in javascript
        // startDate: moment().format(),
        // endDate: ''
        // category:
    };
}

export const incomesSlice = createSlice({
    name: 'incomes',
    initialState: incomesAdapter.getInitialState({
        incomesLoaded: false,
        filtersLoaded: false,
        status: 'idle',
        incomeParams: initParams(),
        metaData: null
    }),
    reducers: {
        setIncomeParams: (state, action) => {
            state.productsLoaded = false;
            state.productParams = {...state.productParams, ...action.payload, pageNumber: 1}
        },
        setPageNumber: (state, action) => {
            state.productsLoaded = false;
            state.productParams = {...state.productParams, ...action.payload}
        },
        setMetaData: (state, action) => {
            state.metaData = action.payload
        },
        resetIncomeParams: (state) => {
            state.productParams = initParams()
        },
        setIncome: (state, action) => {
            incomesAdapter.upsertOne(state, action.payload);
            state.productsLoaded = false;
        },
        removeIncome: (state, action) => {
            incomesAdapter.removeOne(state, action.payload);
            state.productsLoaded = false;
        }
    },
    extraReducers: (builder => {
        builder.addCase(fetchIncomesAsync.pending, (state, action) => {
            state.status = 'pendingFetchIncomes'
        });
        builder.addCase(fetchIncomesAsync.fulfilled, (state, action) => {
            incomesAdapter.setAll(state, action.payload);
            state.status = 'idle';
            state.productsLoaded = true;
        });
        builder.addCase(fetchIncomesAsync.rejected, (state, action) => {
            console.log(action.payload);
            state.status = 'idle';
        });
        // builder.addCase(fetchIncomeAsync.pending, (state) => {
        //     state.status = 'pendingFetchIncome';
        // });
        // builder.addCase(fetchIncomeAsync.fulfilled, (state, action) => {
        //     incomesAdapter.upsertOne(state, action.payload);
        //     state.status = 'idle';
        // });
        // builder.addCase(fetchIncomeAsync.rejected, (state, action) => {
        //     console.log(action);
        //     state.status = 'idle';
        // });
        // builder.addCase(fetchFilters.pending, (state) => {
        //     state.status = 'pendingFetchFilters';
        // });
        // builder.addCase(fetchFilters.fulfilled, (state, action) => {
        //     state.brands = action.payload.brands;
        //     state.types = action.payload.types;
        //     state.status = 'idle';
        //     state.filtersLoaded = true;
        // });
        // builder.addCase(fetchFilters.rejected, (state) => {
        //     state.status = 'idle';
        // });
    })
})