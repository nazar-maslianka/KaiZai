using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace KaiZai.Services.Incomes.CL.Consumers;

public sealed class CategoryDeletedConsumer : IConsumer<CategoryDeleted>
{
    private readonly ILogger<CategoryDeletedConsumer> _logger;
    private readonly ICategoryConsumersService _categoryConsumersService;

    public CategoryDeletedConsumer(
        ILogger<CategoryDeletedConsumer> logger,
        ICategoryConsumersService categoryConsumersService)
    {
        _logger = logger;
        _categoryConsumersService = categoryConsumersService;
    }

    public async Task Consume(ConsumeContext<CategoryDeleted> context)
    {
        var message = context.Message;

        var result = await _categoryConsumersService.DeleteCategoryAsync(message);
        if (!result.IsSuccess)
        {
            _logger.LogWarning(result.Error);
        }
    }
}