using GreenPipes;
using KaiZai.Common.Types;

namespace KaiZai.Service.Expenses.CL.Filters;

public sealed class ExpenseCategoriesConsumersFilter<T> :
    IFilter<ConsumeContext<T>> where T : class, IBaseCategoryContract
{
    public void Probe(ProbeContext context) {}

    public async Task Send(ConsumeContext<T> context, IPipe<ConsumeContext<T>> next)
    {
        if (context.Message.CategoryType == CategoryType.Expense)
        {
            await next.Send(context);
        }
    }
}