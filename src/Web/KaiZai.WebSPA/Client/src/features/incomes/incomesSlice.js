/* eslint-disable no-unused-vars */
import { createEntityAdapter, createSelector, createSlice, useDispatch } from "@reduxjs/toolkit";
import { apiSlice } from "../../app/api/apiSlice.js";
import { format, subMonths, startOfMonth, isFirstDayOfMonth } from "date-fns";

// Entity adapter
const incomesAdapter = createEntityAdapter({
  selectId: (income) => income.id
});

// Initial state for incomes data view settings
const incomesViewSettingsInitialState = {
  incomesLoaded: false,
  filtersLoaded: false,
  pagingParams: initPaginationParams(),
  filteringParams: initFilteringParams(),
};

// Initial state for incomes data
const incomesInitialState = incomesAdapter.getInitialState({
  metadata: null
});

//TODO: change test values to original 
// Helper function to get default date limit
function getDefaultDateLimit(isLowerLimit = true) {
  let newDate = new Date(Date.now());

  if (isLowerLimit && !isFirstDayOfMonth(newDate)) {
    newDate = new Date("2023-01-01");
    //subMonths(startOfMonth(newDate),1);
  } else if (isLowerLimit) {
    newDate = new Date("2023-01-01");
    //subMonths(newDate,1);
  }
  console.log(newDate);
  return format(newDate, 'yyyy-MM-dd');
}

const formatDateToString = (dateValue) => format(dateValue, 'yyyy-MM-dd');

// Helper function to initialize pagination parameters
function initPaginationParams() {
  return {
    pageNumber: 1,
    pageSize: 25,
  };
}

// Helper function to initialize filtering parameters
function initFilteringParams() {
  return {
    startDate: getDefaultDateLimit(),
    endDate: getDefaultDateLimit(false)
  };
}

// API endpoint configuration
const featureBaseUrlPath = '/incomes';

export const extendedApiSlice = apiSlice.injectEndpoints({
  tagTypes: ['Post'],
  endpoints: (builder) => ({
    getPaginatedIncomes: builder.query({
      query({ pagingParams, filteringParams }) {
        console.log(pagingParams);
        const { pageNumber = 1, pageSize = 25 } = pagingParams || {};
        const { startDate = getDefaultDateLimit(), endDate = getDefaultDateLimit(false) } = filteringParams || {};
        return `${featureBaseUrlPath}?pageNumber=${pageNumber}&pageSize=${pageSize}&startDate=${startDate}&endDate=${endDate}`;
      },
      transformResponse: (responseData) => {
        const { items } = responseData;
        return incomesAdapter.setAll(incomesInitialState, items);
      },
      onQueryStarted: async (id, { dispatch, queryFulfilled }) => {
        const data = await queryFulfilled;
        dispatch(setMetadata(data));
      }
    })
  })
});

const setMetadata = ({ metadata }) => ({ type: 'SET_METADATA', metadata });

const incomesPartialReducer = (state = incomesInitialState, action) => {
  switch (action.type) {
    case 'SET_METADATA':
      return { ...state, metadata: action.payload };
    default:
      return state;
  }
};

//Incomes data view settings slice(mostly for IncomesTransactions page)
const incomesDataViewSettingsSlice = createSlice({
  name: 'incomesDataViewSettings',
  initialState: incomesViewSettingsInitialState,
  reducers: {
    setPageSize: (state, action) => {
      state.pagingParams.pageSize = action.payload;
    },
    setPageNumber: (state, action) => {
      state.pagingParams.pageNumber = action.payload;
    },
    setDateRange: (state, action) => {
      console.log(action.payload);
      state.filteringParams = { ...state.filteringParams, ...action.payload };
    }
  },
});

// Export actions from dataViewSettingsSlice
export const {
  setPageSize,
  setPageNumber,
  setDateRange
} = incomesDataViewSettingsSlice.actions;

// Export API endpoints
export const {
  useGetPaginatedIncomesQuery
} = extendedApiSlice;

const selectIncomesData = (state, parameters) => {
  const selectResult = extendedApiSlice.endpoints.getPaginatedIncomes.select(parameters);
  const incomesResult = selectResult(state);
  //TODO: think about another statuses
  if (incomesResult.status === 'fulfilled') {
    return incomesResult.data;
  }
};

export const selectAllIncomes = createSelector(
  [selectIncomesData],
  (incomesResult) => {
    const data = incomesResult ? incomesAdapter.getSelectors().selectAll(incomesResult) : [];
    return data;
  }
);

export default incomesDataViewSettingsSlice.reducer;