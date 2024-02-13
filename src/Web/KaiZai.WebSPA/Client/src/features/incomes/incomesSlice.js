import { createSlice, createEntityAdapter, createSelector } from "@reduxjs/toolkit";
import { apiSlice } from "../../app/api/apiSlice.js";
// import { sub } from 'date-fns';

const incomesAdapter = createEntityAdapter();

const initialState = incomesAdapter.getInitialState({
    incomesLoaded: false,
    filtersLoaded: false,
    pagingParams: initPaginationParams(),
    filteringParams: initFilteringParams(),
    metaData: null
});

function getDefaultDate (isEndPeriod = false) {
    const date = new Date(Date.now());
    const year = date.getFullYear()-1;
    const month = String(date.getMonth() + (isEndPeriod ? 12 : 1)).padStart(2, '0');
    const day = String(date.getDate()+6).padStart(2, '0');

    return `${year}-${month}-${day}`;
}

function initPaginationParams() { 
    return {
        pageNumber: 1,
        pageSize: 25,
     };
}

function initFilteringParams() { 
    return {
        startDate: getDefaultDate(false),
        endDate: getDefaultDate(true)
    }
}

const featureBaseUrlPath = '/incomes';

export const extendedApiSlice = apiSlice.injectEndpoints({
    endpoints: builder => ({
        //api/incomes?pageNumber=1&pageSize=10&startDate=2023-01-01T00:00:00&endDate=2023-06-30T23:59:59
        getPaginatedIncomes: builder.query({
            query() {
                const pagingParams = initialState.pagingParams;
                const filteringParams = initialState.filteringParams;
                return `${featureBaseUrlPath}/?pageNumber=${pagingParams.pageNumber}&pageSize=${pagingParams.pageSize}&startDate=${filteringParams.startDate}&endDate=${filteringParams.endDate}`;
            },
            onQueryStarted: async (id, { queryFulfilled }) => {
                const { data } = await queryFulfilled;
                incomesAdapter.setAll(initialState.entities, data);
                incomesAdapter.setOne(initialState.metaData, data.metaData)
            },
            providesTags: (result) => [
                { type: 'Incomes', id: "LIST" },
                ...result.ids.map(id => ({ type: 'Income', id }))
            ]
        })
    })
});

 export const incomesSlice = createSlice({
     name: 'incomes',
     initialState: initialState,
     reducers: {
         setPaginationParams: (state, action) => {
            state.incomesLoaded = false;
            state.incomeParams = {...state.incomeParams, ...action.payload, pageNumber: 1}
         },
          setPageNumber: (state, action) => {
              state.incomesLoaded = false;
              state.incomeParams = {...state.incomeParams, ...action.payload}
          },
          setMetaData: (state, action) => {
              state.metaData = action.payload
          },
         // resetIncomeParams: (state) => {
         //     state.incomeParams = initParams()
         // },
         // setIncome: (state, action) => {
         //     incomesAdapter.upsertOne(state, action.payload);
         //     state.productsLoaded = false;
         // },
         // removeIncome: (state, action) => {
         //     incomesAdapter.removeOne(state, action.payload);
         //     state.productsLoaded = false;
         // }
     },  
 })

export const { setPaginationParams, setPageNumber, setMetaData } = incomesSlice.actions

export const {
    useGetPaginatedIncomesQuery,
    useGetPostsByUserIdQuery,
} = extendedApiSlice

export const selectIncomesResult = extendedApiSlice.endpoints.getPaginatedIncomes.select()

// Creates memoized selector
const selectIncomesData = createSelector(
    selectIncomesResult,
    incomesResult => incomesResult.data // normalized state object with ids & entities
)

//getSelectors creates these selectors and we rename them with aliases using destructuring
export const {
    selectAll: selectAllIncomes,
    selectById: selectIncomeById,
    selectIds: selectIncomeIds
    // Pass in a selector that returns the posts slice of state
} = incomesAdapter.getSelectors(state => selectIncomesData(state) ?? initialState.entities)
