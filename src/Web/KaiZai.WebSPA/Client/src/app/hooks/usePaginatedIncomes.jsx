import { useEffect } from "react";
import { useSelector } from "react-redux";
import {
  useGetPaginatedIncomesQuery,
  selectAllIncomes} from "../../features/incomes/incomesSlice";

export const usePaginatedIncomes = () => {
  const { pagingParams, filteringParams } = useSelector((state) => state["incomesDataViewSettings"]);
  const { refetch } = useGetPaginatedIncomesQuery({ pagingParams, filteringParams });
  
  // useEffect to refetch only when the relevant dependencies change
  useEffect(() => {
    refetch();
    }, [pagingParams, filteringParams, refetch]);
  
    // Use the selector with the updated values from the API response
    const paginatedIncomes = useSelector((state) => selectAllIncomes(state, { pagingParams, filteringParams }));
  
    return { paginatedIncomes, refetch };
  };