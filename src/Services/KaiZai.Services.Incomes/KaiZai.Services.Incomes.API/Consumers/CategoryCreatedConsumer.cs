using KaiZai.Service.Categories.Contracts;
using MassTransit;

namespace KaiZai.Services.Incomes.API.Consumers;

public sealed class CategoryCreatedConsumer : IConsumer<CategoryCreated>
{
    public CategoryCreatedConsumer()
    {
        
    }

    //Test method
    //TODO: change logic later
    public Task Consume(ConsumeContext<CategoryCreated> context)
    {
        Console.WriteLine($"Id: {context.Message.Id} \r\n ProfileId: {context.Message.ProfileId} \r\n Name: {context.Message.Name} \r\n CategoryType: {context.Message.CategoryType}");

        return Task.FromResult("1");
    }
}