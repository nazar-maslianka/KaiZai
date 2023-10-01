using KaiZai.Services.Incomes.BAL.DTOs;
using KaiZai.Services.Incomes.DAL.Models;

namespace KaiZai.Services.Incomes.BAL.Services;

//TODO: Change later according to importance
public interface IIncomeService
{
// Create a new income record
    Task<Guid> CreateIncomeAsync(Income income);

    // Update an existing income record
    //Task UpdateIncomeAsync(Guid incomeId, Income updatedIncome);
//
    //// Delete an income record
    //Task DeleteIncomeAsync(Guid incomeId);
//
    //// Retrieve a single income record by its ID
    //Task<Income> GetIncomeByIdAsync(Guid incomeId);
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