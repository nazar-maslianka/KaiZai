namespace KaiZai.Services.Incomes.BAL.Services;

public sealed class IncomesService : IIncomesService
{
    public IncomesService(IIncomesRepository incomesRepository)
    {
        _incomesRepository = incomesRepository;
    }
}