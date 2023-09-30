namespace KaiZai.Services.Incomes.DAL.Models;

public class Income : IEntity
{
    public Guid Id { get; set; }
    public Guid ProfileId { get; set; } 
    public Guid CategoryId { get; set; } 
    public DateTimeOffset IncomeDate { get; set; }
    public string Description { get; set; }
    public decimal Amount { get; set; }
}