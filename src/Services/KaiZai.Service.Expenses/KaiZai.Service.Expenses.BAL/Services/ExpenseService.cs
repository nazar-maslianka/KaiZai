using System.Linq.Expressions;
using KaiZai.Service.Expenses.BAL.DTOs;
using KaiZai.Service.Expenses.DAL.Models;
using MongoDB.Driver;

namespace KaiZai.Service.Expenses.BAL.Services;

public sealed class ExpenseService : IExpenseService
{
    private readonly IExpenseRepository _expenseRepository;
    private readonly ICategoryRepository _categoryRepository;

    public ExpenseService(IExpenseRepository expenseRepository,
        ICategoryRepository categoryRepository)
    {
        _expenseRepository = expenseRepository  ?? throw new ArgumentNullException(nameof(expenseRepository));
        _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
    }
    
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
    /// <exception cref="ArgumentException">Thrown when <paramref name="profileId"/> is <see cref="Guid.Empty"/>.</exception>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="addExpenseDTO"/> is null.</exception>

    public async Task<Result> AddExpenseAsync(Guid profileId, 
        AddUpdateExpenseDTO addExpenseDTO)
    {
        if (profileId == Guid.Empty)
        {
            throw new ArgumentException("Invalid profileId", nameof(profileId));
        }

        if (addExpenseDTO == null)
        {
            throw new ArgumentNullException("Invalid addExpenseDTO", nameof(addExpenseDTO));
        }

        var expense = addExpenseDTO.ToExpense(profileId);
        await _expenseRepository.CreateAsync(expense);
        
        if (expense.Id == Guid.Empty)
        {
            return Result.Failure("An expense record is not created in the database");
        }

        return Result.Success();
    }

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
    /// <exception cref="ArgumentException">Thrown when <paramref name="profileId"/> or <paramref name="expenseId"/> is <see cref="Guid.Empty"/>.</exception>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="updateExpenseDTO"/> is null.</exception>

    public async Task<Result> UpdateExpenseAsync(Guid profileId,
        Guid expenseId, 
        AddUpdateExpenseDTO updateExpenseDTO)
    {
        if (profileId == Guid.Empty)
        {
            throw new ArgumentException("Invalid profileId", nameof(profileId));
        }

        if (expenseId == Guid.Empty)
        {
            throw new ArgumentException("Invalid expenseId", nameof(expenseId));
        }

        if (updateExpenseDTO == null)
        {
            throw new ArgumentNullException("Invalid updateExpenseDTO", nameof(updateExpenseDTO));
        }

        var expense = updateExpenseDTO.ToExpense(profileId, expenseId);
        await _expenseRepository.UpdateAsync(expense);
        
        return Result.Success();
    }

    /// <summary>
    /// Deletes an expense record asynchronously by its unique identifier.
    /// </summary>
    /// <param name="expenseId">The unique identifier of the expense record to delete.</param>
    /// <returns>
    /// A <see cref="Task{TResult}"/> representing the asynchronous operation.
    /// The task result contains a <see cref="Result"/> indicating whether the deletion was successful or not.
    /// </returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="expenseId"/> is <see cref="Guid.Empty"/>.</exception>

    public async Task<Result> DeleteExpenseAsync(Guid expenseId)
    {
        if (expenseId == Guid.Empty)
        {
            throw new ArgumentException("Invalid expenseId", nameof(expenseId));
        }

        await _expenseRepository.RemoveAsync(expenseId);
        return Result.Success();
    }
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
    /// <exception cref="ArgumentException">Thrown when <paramref name="expenseId"/> is <see cref="Guid.Empty"/>.</exception>
    public async Task<Result<ExpenseDTO>> GetExpenseByIdAsync(Guid expenseId)
    {
        if (expenseId == Guid.Empty)
        {
            throw new ArgumentException("Invalid expenseId", nameof(expenseId));
        }
        var expense = await _expenseRepository.GetOneAsync(expenseId);
  
        if (expense == null)
        {
            return Result<ExpenseDTO>.Failure($"Expense by id: {expenseId} not found in database.");
        }

        var category = _categoryRepository.GetOne(expense.CategoryId);
        
        if (category == null)
        {
            return Result<ExpenseDTO>.Failure($"Category by id: {expense.CategoryId} not found in database.");
        }
        var expenseDTO = expense.ToExpenseDTO(category);

        return Result<ExpenseDTO>.Success(expenseDTO); 
    }

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
    /// <exception cref="ArgumentException">Thrown when <paramref name="profileId"/> is <see cref="Guid.Empty"/>.</exception>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="pagingParams"/> is null.</exception>

    public async Task<Result<PagedList<ExpenseShortDTO>>> GetExpensesAggregatedByPageAsync(Guid profileId,
        PagingParams pagingParams,
        FilteringParams? filteringParams = null) 
    {
        if (profileId == Guid.Empty)
        {
            throw new ArgumentException("Invalid profileId", nameof(profileId));
        }

        if (pagingParams == null)
        {
            throw new ArgumentNullException("Invalid pagingParams", nameof(pagingParams));
        }
        
        filteringParams = filteringParams ?? new FilteringParams();

        try
        {
            Expression<Func<Expense, bool>>? filterDefinition = expense =>
                expense.ProfileId.Equals(profileId)
                && expense.ExpenseDate >= filteringParams.StartDate
                && expense.ExpenseDate <= filteringParams.EndDate;

            var baseAggregateQuery = _expenseRepository
                .GetBaseAggregateQuery(filterDefinition)
                .SortBy(inc => inc.ExpenseDate.Date);

            var aggregatedByPageExpensesResult = _expenseRepository.GetAggregateByPageAsync(
                pagingParams.PageSize,
                pagingParams.PageNumber,
                baseAggregateQuery);

            var allCategories = _categoryRepository
                .GetAllAsync(x => x.ProfileId == profileId);

            await Task.WhenAll(aggregatedByPageExpensesResult, allCategories);

            var pageWithExpenses = aggregatedByPageExpensesResult.Result.Data
                .Select(inc => inc.ToExpenseShortDTO(allCategories
                    .Result.FirstOrDefault(cat => cat.Id == inc.CategoryId)))
                .ToList();

            var totalExpensesCount = aggregatedByPageExpensesResult.Result.Count;

            return Result<PagedList<ExpenseShortDTO>>.Success(
                new PagedList<ExpenseShortDTO>
                (
                    pageWithExpenses,
                    totalExpensesCount,
                    pagingParams.PageNumber,
                    pagingParams.PageSize
                )
            );
        }

        catch(Exception ex)
        {
            return Result<PagedList<ExpenseShortDTO>>.Failure(null, ex.Message.ToString());
        }
    }
    #endregion
}