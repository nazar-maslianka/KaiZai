using KaiZai.Service.Incomes.BAL.Core;
using KaiZai.Service.Incomes.BAL.DTOs;

namespace KaiZai.Service.Incomes.BAL.Contracts;

//TODO: Change later according to importance
public interface IIncomeService
{
    #region CRUD operations
      /// <summary>
    /// Adds a new income record asynchronously.
    /// </summary>
    /// <param name="profileId">The unique identifier of the profile to associate with the income.</param>
    /// <param name="addIncomeDTO">The data to create the new income record.</param>
    /// <returns>
    /// A <see cref="Task{TResult}"/> representing the asynchronous operation.
    /// The task result contains a <see cref="Result"/> indicating whether the operation was successful or not.
    /// </returns>
    Task<Result> AddIncomeAsync(Guid profileId, 
        AddUpdateIncomeDTO addIncomeDTO);

    /// <summary>
    /// Updates an existing income record asynchronously.
    /// </summary>
    /// <param name="profileId">The unique identifier of the profile associated with the income.</param>
    /// <param name="incomeId">The unique identifier of the income record to update.</param>
    /// <param name="updateIncomeDTO">The data to update the existing income record.</param>
    /// <returns>
    /// A <see cref="Task{TResult}"/> representing the asynchronous operation.
    /// The task result contains a <see cref="Result"/> indicating whether the operation was successful or not.
    /// </returns>
    Task<Result> UpdateIncomeAsync(Guid profileId, 
        Guid incomeId, 
        AddUpdateIncomeDTO updateIncomeDTO);

    /// <summary>
    /// Deletes an income record asynchronously by its unique identifier.
    /// </summary>
    /// <param name="incomeId">The unique identifier of the income record to delete.</param>
    /// <returns>
    /// A <see cref="Task{TResult}"/> representing the asynchronous operation.
    /// The task result contains a <see cref="Result"/> indicating whether the deletion was successful or not.
    /// </returns>
    Task<Result> DeleteIncomeAsync(Guid incomeId);
    #endregion
    
    #region Get operations
    /// <summary>
    /// Gets an income record by its unique identifier asynchronously.
    /// </summary>
    /// <param name="incomeId">The unique identifier of the income record to retrieve.</param>
    /// <returns>
    /// A <see cref="Task{TResult}"/> representing the asynchronous operation.
    /// The task result contains a <see cref="Result{T}"/> where T is <see cref="IncomeDTO"/>.
    /// The result will be successful with the <see cref="IncomeDTO"/> if found, or a failure result with an error message if not found.
    /// </returns>
    Task<Result<IncomeDTO>> GetIncomeByIdAsync(Guid incomeId);

    /// <summary>
    /// Retrieves a paged and aggregated list of income records asynchronously based on the provided parameters.
    /// </summary>
    /// <param name="profileId">The unique identifier of the profile associated with the incomes.</param>
    /// <param name="pagingParams">The parameters for paging and ordering the results.</param>
    /// <param name="filteringParams">Optional parameters for filtering the results by date range.</param>
    /// <returns>
    /// A <see cref="Task{TResult}"/> representing the asynchronous operation.
    /// The task result contains a <see cref="Result{T}"/> containing the paged and aggregated list of income records.
    /// </returns>
    Task<Result<PagedList<IncomeShortDTO>>> GetIncomesAggregatedByPageAsync(Guid profileId,
        PagingParams pagingParams,
        FilteringParams? filteringParams = null);
    //
    //// Retrieve a list of income records for a specific user
    ////Task<List<Income>> GetIncomesByUserIdAsy Task<Guid> CreateIncomeAsync(Income income);
    //
    //// Retrieve a list of income records by filters for a specific user
    //Task<IReadOnlyList<IncomeShortDTO>> GetIncomesAggregatedByPageAsync(Guid userId);
    //
    //// Retrieve a list of income records for a specific category
    //Task<List<Income>> GetIncomesByCategoryIdAsync(Guid categoryId);
    //
    //// Retrieve a list of income records within a date range
    //Task<List<Income>> GetIncomesByDateRangeAsync(DateTimeOffset startDate, DateTimeOffset endDate);
    //
    //// Calculate the total income for a specific user
    //Task<decimal> CalculateTotalIncomeAsync(Guid userId);
    //
    //// Calculate the total income for a specific category
    //Task<decimal> CalculateTotalIncomeByCategoryAsync(Guid categoryId);
    //
    //// Get the average income amount for a specific user
    //Task<decimal> GetAverageIncomeAsync(Guid userId);
    //
    //// Search income records by a keyword in the description
    //Task<List<Income>> SearchIncomesByDescriptionAsync(string keyword);
    //
    //// Retrieve the most recent income records
    //Task<List<Income>> GetRecentIncomesAsync(int count);
    #endregion
    //
    //// Sort income records by date or amount
    //Task<List<Income>> SortIncomesAsync(string sortBy);
    //
    //// Group income records by category
    //Task<Dictionary<string, List<Income>>> GroupIncomesByCategoryAsync();
    //
    //// Export income records to a CSV file
    //Task<byte[]> ExportIncomesToCsvAsync();
    //
    //// Import income records from a CSV file
    //Task ImportIncomesFromCsvAsync(byte[] csvData);

}