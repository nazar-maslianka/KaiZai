using KaiZai.Service.Categories.Contracts;
using KaiZai.Services.Incomes.BAL.Services;
using MassTransit;

namespace KaiZai.Services.Incomes.API.Consumers;

public sealed class CategoryCreatedConsumer : IConsumer<CategoryCreated>
{
    private readonly ICategoryConsumersService _categoryConsumersService;

    public CategoryCreatedConsumer(ICategoryConsumersService categoryConsumersService)
    {
        _categoryConsumersService = categoryConsumersService;
    }

    //TODO: change logic later
    public async Task Consume(ConsumeContext<CategoryCreated> context)
    {
        var message = context.Message;


        //if ()
        //{
        //    
        //}

        await _categoryConsumersService.CreateCategoryAsync(message);
    }
}