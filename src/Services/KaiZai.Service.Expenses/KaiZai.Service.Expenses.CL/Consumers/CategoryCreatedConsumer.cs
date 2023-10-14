using KaiZai.Service.Expenses.BAL.Core;

namespace KaiZai.Service.Expenses.CL.Consumers;

public sealed class CategoryCreatedConsumer : IConsumer<CategoryCreated>
{
    private readonly ILogger<CategoryCreatedConsumer> _logger;
    private readonly ICategoryConsumersService _categoryConsumersService;

    public CategoryCreatedConsumer(
        ILogger<CategoryCreatedConsumer> logger,
        ICategoryConsumersService categoryConsumersService)
    {
        _logger = logger;
        _categoryConsumersService = categoryConsumersService;
    }

    public async Task Consume(ConsumeContext<CategoryCreated> context)
    {
        var message = context.Message;

        var result = await _categoryConsumersService.CreateCategoryAsync(message);
        if (result.ProcessStatus == ProcessStatus.UserError)
        {
            _logger.LogWarning(result.UserError);
        }
    }
}