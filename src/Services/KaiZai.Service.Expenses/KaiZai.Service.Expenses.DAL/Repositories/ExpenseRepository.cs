namespace KaiZai.Service.Expenses.DAL.Repositories;

public sealed class ExpenseRepository : MongoRepository<Expense>, IExpenseRepository
{
    public ExpenseRepository(IMongoDatabase mongoDatabase, string collectionName = nameof(Models.Expense)) 
        : base(mongoDatabase, collectionName)
    {
    }
}