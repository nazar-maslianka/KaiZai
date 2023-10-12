using KaiZai.Service.Incomes.BAL.Core;

namespace KaiZai.Service.Incomes.CL.Consumers;

public sealed class CategoryUpdatedConsumer: IConsumer<CategoryUpdated>
{
    private readonly ILogger<CategoryUpdatedConsumer> _logger;
    private readonly ICategoryConsumersService _categoryConsumersService;

    public CategoryUpdatedConsumer(
        ILogger<CategoryUpdatedConsumer> logger,
        ICategoryConsumersService categoryConsumersService)
    {
        _logger = logger;
        _categoryConsumersService = categoryConsumersService;
    }

    public async Task Consume(ConsumeContext<CategoryUpdated> context)
    {
        var message = context.Message;

        var result = await _categoryConsumersService.UpdateCategoryAsync(message);
        if (result.ProcessStatus == ProcessStatus.UserError)
        {
            _logger.LogWarning(result.UserError);
        }
    } 
}