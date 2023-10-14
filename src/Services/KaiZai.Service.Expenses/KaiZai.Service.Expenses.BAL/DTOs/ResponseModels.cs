namespace KaiZai.Service.Expenses.BAL.DTOs;

public record CategoryDTO(Guid Id, string Name, CategoryType CategoryType);
public record CategoryShortDTO(Guid Id, string Name);
public record ExpenseShortDTO(Guid Id, CategoryShortDTO Category, DateTimeOffset ExpenseDate, decimal Amount);
public record ExpenseDTO(Guid Id, Guid ProfileId, CategoryDTO Category, DateTimeOffset ExpenseDate, string Description, decimal Amount);