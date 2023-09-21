using KaiZai.Services.Incomes.DAL.Repositories;

namespace KaiZai.Services.Incomes.BAL.Services;

public sealed class IncomeService : IIncomeService
{
    private readonly IIncomeRepository _incomesRepository;

    public IncomeService(IIncomeRepository incomesRepository)
    {
        _incomesRepository = incomesRepository;
    }
}