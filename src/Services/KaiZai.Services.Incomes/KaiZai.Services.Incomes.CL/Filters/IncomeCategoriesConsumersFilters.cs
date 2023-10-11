using GreenPipes;
using KaiZai.Common.Types;

namespace KaiZai.Services.Incomes.CL.Filters;

public sealed class IncomeCategoriesConsumersFilters<T>
    : IFilter<ConsumeContext<CategoryCreated>>,
    IFilter<ConsumeContext<CategoryUpdated>>,
    IFilter<ConsumeContext<CategoryDeleted>>
{
    public void Probe(ProbeContext context) {}

    public async Task Send(ConsumeContext<CategoryCreated> context, IPipe<ConsumeContext<CategoryCreated>> next)
    {
        if (context.Message.CategoryType == CategoryType.Income)
        {
            await next.Send(context);
        }
    }
    public async Task Send(ConsumeContext<CategoryUpdated> context, IPipe<ConsumeContext<CategoryUpdated>> next)
    {
        if (context.Message.CategoryType == CategoryType.Income)
        {
            await next.Send(context);
        }
    }
    public async Task Send(ConsumeContext<CategoryDeleted> context, IPipe<ConsumeContext<CategoryDeleted>> next)
    {
        if (context.Message.CategoryType == CategoryType.Income)
        {
            await next.Send(context);
        }
    }
}