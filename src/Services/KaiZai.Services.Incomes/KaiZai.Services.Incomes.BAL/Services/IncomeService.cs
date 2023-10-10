using System.Linq.Expressions;
using KaiZai.Services.Incomes.BAL.Core;
using KaiZai.Services.Incomes.BAL.DTOs;
using KaiZai.Services.Incomes.DAL.Models;
using KaiZai.Services.Incomes.DAL.Repositories;
using MongoDB.Driver;

namespace KaiZai.Services.Incomes.BAL.Services;

public sealed class IncomeService : IIncomeService
{
    private readonly IIncomeRepository _incomeRepository;
    private readonly ICategoryRepository _categoryRepository;

    public IncomeService(IIncomeRepository incomeRepository,
        ICategoryRepository categoryRepository)
    {
        _incomeRepository = incomeRepository  ?? throw new ArgumentNullException(nameof(incomeRepository));
        _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
    }
    
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
    /// <exception cref="ArgumentException">Thrown when <paramref name="profileId"/> is <see cref="Guid.Empty"/>.</exception>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="addIncomeDTO"/> is null.</exception>

    public async Task<Result> AddIncomeAsync(Guid profileId, 
        AddUpdateIncomeDTO addIncomeDTO)
    {
        if (profileId == Guid.Empty)
        {
            throw new ArgumentException("Invalid profileId", nameof(profileId));
        }

        if (addIncomeDTO == null)
        {
            throw new ArgumentNullException("Invalid addIncomeDTO", nameof(addIncomeDTO));
        }

        var income = addIncomeDTO.ToIncome(profileId);
        await _incomeRepository.CreateAsync(income);
        
        if (income.Id == Guid.Empty)
        {
            return Result.Failure("An income record is not created in the database");
        }

        return Result.Success();
    }

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
    /// <exception cref="ArgumentException">Thrown when <paramref name="profileId"/> or <paramref name="incomeId"/> is <see cref="Guid.Empty"/>.</exception>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="updateIncomeDTO"/> is null.</exception>

    public async Task<Result> UpdateIncomeAsync(Guid profileId,
        Guid incomeId, 
        AddUpdateIncomeDTO updateIncomeDTO)
    {
        if (profileId == Guid.Empty)
        {
            throw new ArgumentException("Invalid profileId", nameof(profileId));
        }

        if (incomeId == Guid.Empty)
        {
            throw new ArgumentException("Invalid incomeId", nameof(incomeId));
        }

        if (updateIncomeDTO == null)
        {
            throw new ArgumentNullException("Invalid updateIncomeDTO", nameof(updateIncomeDTO));
        }

        var income = updateIncomeDTO.ToIncome(profileId, incomeId);
        await _incomeRepository.UpdateAsync(income);
        
        return Result.Success();
    }

    /// <summary>
    /// Deletes an income record asynchronously by its unique identifier.
    /// </summary>
    /// <param name="incomeId">The unique identifier of the income record to delete.</param>
    /// <returns>
    /// A <see cref="Task{TResult}"/> representing the asynchronous operation.
    /// The task result contains a <see cref="Result"/> indicating whether the deletion was successful or not.
    /// </returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="incomeId"/> is <see cref="Guid.Empty"/>.</exception>

    public async Task<Result> DeleteIncomeAsync(Guid incomeId)
    {
        if (incomeId == Guid.Empty)
        {
            throw new ArgumentException("Invalid incomeId", nameof(incomeId));
        }

        await _incomeRepository.RemoveAsync(incomeId);
        return Result.Success();
    }
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
    /// <exception cref="ArgumentException">Thrown when <paramref name="incomeId"/> is <see cref="Guid.Empty"/>.</exception>
    public async Task<Result<IncomeDTO>> GetIncomeByIdAsync(Guid incomeId)
    {
        if (incomeId == Guid.Empty)
        {
            throw new ArgumentException("Invalid incomeId", nameof(incomeId));
        }
        var income = await _incomeRepository.GetOneAsync(incomeId);
  
        if (income == null)
        {
            return Result<IncomeDTO>.Failure($"Income by id: {incomeId} not found in database.");
        }

        var category = _categoryRepository.GetOne(income.CategoryId);
        
        if (category == null)
        {
            return Result<IncomeDTO>.Failure($"Category by id: {income.CategoryId} not found in database.");
        }
        var incomeDTO = income.ToIncomeDTO(category);

        return Result<IncomeDTO>.Success(incomeDTO); 
    }

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
    /// <exception cref="ArgumentException">Thrown when <paramref name="profileId"/> is <see cref="Guid.Empty"/>.</exception>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="pagingParams"/> is null.</exception>

    public async Task<Result<PagedList<IncomeShortDTO>>> GetIncomesAggregatedByPageAsync(Guid profileId,
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
            Expression<Func<Income, bool>>? filterDefinition = income =>
                income.ProfileId.Equals(profileId)
                && income.IncomeDate >= filteringParams.StartDate
                && income.IncomeDate <= filteringParams.EndDate;

            var baseAggregateQuery = _incomeRepository
                .GetBaseAggregateQuery(filterDefinition)
                .SortBy(inc => inc.IncomeDate.Date);

            var aggregatedByPageIncomesResult = _incomeRepository.GetAggregateByPageAsync(
                pagingParams.PageSize,
                pagingParams.PageNumber,
                baseAggregateQuery);

            var allCategories = _categoryRepository
                .GetAllAsync(x => x.ProfileId == profileId);

            await Task.WhenAll(aggregatedByPageIncomesResult, allCategories);

            var pageWithIncomes = aggregatedByPageIncomesResult.Result.Data
                .Select(inc => inc.ToIncomeShortDTO(allCategories
                    .Result.FirstOrDefault(cat => cat.Id == inc.CategoryId)))
                .ToList();

            var totalIncomesCount = aggregatedByPageIncomesResult.Result.Count;

            return Result<PagedList<IncomeShortDTO>>.Success(
                new PagedList<IncomeShortDTO>
                (
                    pageWithIncomes,
                    totalIncomesCount,
                    pagingParams.PageNumber,
                    pagingParams.PageSize
                )
            );
        }

        catch(Exception ex)
        {
            return Result<PagedList<IncomeShortDTO>>.Failure(null, ex.Message.ToString());
        }
    }
    #endregion
}