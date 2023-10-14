using System.ComponentModel.DataAnnotations;

namespace KaiZai.Service.Expenses.BAL.DTOs;

public sealed record AddUpdateExpenseDTO(Guid CategoryId, [Required] DateTimeOffset ExpenseDate, string Description, decimal Amount);