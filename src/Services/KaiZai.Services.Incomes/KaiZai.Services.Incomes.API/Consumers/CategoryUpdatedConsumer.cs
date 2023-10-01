using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KaiZai.Service.Categories.Contracts;
using KaiZai.Services.Incomes.BAL.Services;
using MassTransit;

namespace KaiZai.Services.Incomes.API.Consumers;

public sealed class CategoryUpdatedConsumer: IConsumer<CategoryUpdated>
{
    private readonly ILogger<CategoryDeletedConsumer> _logger;
    private readonly ICategoryConsumersService _categoryConsumersService;

    public CategoryUpdatedConsumer(
        ILogger<CategoryDeletedConsumer> logger,
        ICategoryConsumersService categoryConsumersService)
    {
        _logger = logger;
        _categoryConsumersService = categoryConsumersService;
    }

    public async Task Consume(ConsumeContext<CategoryUpdated> context)
    {
        var message = context.Message;

        var result = await _categoryConsumersService.UpdateCategoryAsync(message);
        if (!result.IsSuccess)
        {
            _logger.LogWarning(result.Error);
        }
    } 
}