namespace KaiZai.Services.Incomes.CL.Consumers;

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
        _logger.LogInformation("Started");
        var message = context.Message;

        var result = await _categoryConsumersService.CreateCategoryAsync(message);
        if (!result.IsSuccess)
        {
            _logger.LogWarning(result.Error);
        }
    }
}