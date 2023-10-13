namespace KaiZai.Service.Expenses.DAL.Models;

public sealed class Expense : IEntity
{
    public Guid Id { get; set; }
    public Guid ProfileId { get; set; } 
    public Guid CategoryId { get; set; } 
    public DateTimeOffset ExpenseDate { get; set; }
    public string Description { get; set; }
    public decimal Amount { get; set; }
}