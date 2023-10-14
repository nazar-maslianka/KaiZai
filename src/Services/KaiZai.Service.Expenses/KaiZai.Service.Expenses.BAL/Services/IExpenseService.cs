using KaiZai.Service.Expenses.BAL.DTOs;

namespace KaiZai.Service.Expenses.BAL.Services;

public interface IExpenseService
{
    #region CRUD operations
      /// <summary>
    /// Adds a new expense record asynchronously.
    /// </summary>
    /// <param name="profileId">The unique identifier of the profile to associate with the expense.</param>
    /// <param name="addExpenseDTO">The data to create the new expense record.</param>
    /// <returns>
    /// A <see cref="Task{TResult}"/> representing the asynchronous operation.
    /// The task result contains a <see cref="Result"/> indicating whether the operation was successful or not.
    /// </returns>
    Task<Result> AddExpenseAsync(Guid profileId, 
        AddUpdateExpenseDTO addExpenseDTO);

    /// <summary>
    /// Updates an existing expense record asynchronously.
    /// </summary>
    /// <param name="profileId">The unique identifier of the profile associated with the expense.</param>
    /// <param name="expenseId">The unique identifier of the expense record to update.</param>
    /// <param name="updateExpenseDTO">The data to update the existing expense record.</param>
    /// <returns>
    /// A <see cref="Task{TResult}"/> representing the asynchronous operation.
    /// The task result contains a <see cref="Result"/> indicating whether the operation was successful or not.
    /// </returns>
    Task<Result> UpdateExpenseAsync(Guid profileId, 
        Guid expenseId, 
        AddUpdateExpenseDTO updateExpenseDTO);

    /// <summary>
    /// Deletes an expense record asynchronously by its unique identifier.
    /// </summary>
    /// <param name="expenseId">The unique identifier of the expense record to delete.</param>
    /// <returns>
    /// A <see cref="Task{TResult}"/> representing the asynchronous operation.
    /// The task result contains a <see cref="Result"/> indicating whether the deletion was successful or not.
    /// </returns>
    Task<Result> DeleteExpenseAsync(Guid expenseId);
    #endregion
    
    #region Get operations
    /// <summary>
    /// Gets an expense record by its unique identifier asynchronously.
    /// </summary>
    /// <param name="expenseId">The unique identifier of the expense record to retrieve.</param>
    /// <returns>
    /// A <see cref="Task{TResult}"/> representing the asynchronous operation.
    /// The task result contains a <see cref="Result{T}"/> where T is <see cref="ExpenseDTO"/>.
    /// The result will be successful with the <see cref="ExpenseDTO"/> if found, or a failure result with an error message if not found.
    /// </returns>
    Task<Result<ExpenseDTO>> GetExpenseByIdAsync(Guid expenseId);

    /// <summary>
    /// Retrieves a paged and aggregated list of expense records asynchronously based on the provided parameters.
    /// </summary>
    /// <param name="profileId">The unique identifier of the profile associated with the expenses.</param>
    /// <param name="pagingParams">The parameters for paging and ordering the results.</param>
    /// <param name="filteringParams">Optional parameters for filtering the results by date range.</param>
    /// <returns>
    /// A <see cref="Task{TResult}"/> representing the asynchronous operation.
    /// The task result contains a <see cref="Result{T}"/> containing the paged and aggregated list of expense records.
    /// </returns>
    Task<Result<PagedList<ExpenseShortDTO>>> GetExpensesAggregatedByPageAsync(Guid profileId,
        PagingParams pagingParams,
        FilteringParams? filteringParams = null);
    #endregion
}