using Ambev.DeveloperEvaluation.Application.Orders.CreateOrder;
using Ambev.DeveloperEvaluation.Application.Orders.Model;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData;

public static class CreateOrderHandlerTestData
{
    private static readonly Faker<OrderItemCommand> createOrderItemHandlerFaker = new Faker<OrderItemCommand>()
        .RuleFor(u => u.ProductId, f => Guid.NewGuid())
        .RuleFor(u => u.Quantity, f => f.Random.Number(1, 20));

    //private static readonly Faker<CreateOrderCommand> createOrderHandlerFaker = new Faker<CreateOrderCommand>()
    //    .CustomInstantiator(p => new CreateOrderCommand(createOrderItemHandlerFaker.GenerateLazy(5).ToList()));


    public static CreateOrderCommand GenerateValidCommand()
    {
        var orderItems = createOrderItemHandlerFaker.GenerateLazy(5).ToList();
        return new CreateOrderCommand() { OrderItems = orderItems, Branch = "Loja teste", 
            UserId = "18df26ad-e70e-4162-8be4-d346e47afc7a", Date = DateTime.UtcNow.ToString() };
    }
}
