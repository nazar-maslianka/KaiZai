using GreenPipes;
using KaiZai.Common.Types;

namespace KaiZai.Service.Incomes.CL.Filters;

public sealed class IncomeCategoriesConsumersFilters<T> :
    IFilter<ConsumeContext<T>> where T : class, IBaseCategoryContract
{
    public void Probe(ProbeContext context) {}

    public async Task Send(ConsumeContext<T> context, IPipe<ConsumeContext<T>> next)
    {
        if (context.Message.CategoryType == CategoryType.Income)
        {
            await next.Send(context);
        }
    }
}