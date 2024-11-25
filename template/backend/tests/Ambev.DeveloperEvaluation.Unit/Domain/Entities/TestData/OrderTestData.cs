using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

public static class OrderTestData
{
    private static readonly Faker<OrderItem> OrderItemFaker = new Faker<OrderItem>()
        .RuleFor(u => u.Id, f => Guid.NewGuid())
        .RuleFor(u => u.ProductId, f => Guid.NewGuid())
        .RuleFor(u => u.Quantity, f => f.Random.Number(1, 19))
        .RuleFor(u => u.TotalAmount, f => new Money(f.Random.Number(1, 500)));

    /// <summary>
    /// Configures the Faker to generate valid Order entities.
    /// The generated Orders will have valid:
    /// - Ordername (using internet Ordernames)
    /// </summary>
    private static readonly Faker<Order> OrderFaker = new Faker<Order>()
        .CustomInstantiator(p => new Order(Guid.NewGuid(), OrderItemFaker.GenerateLazy(5).ToList()))
        .RuleFor(u => u.Id, f => Guid.NewGuid())
        .RuleFor(u => u.UserId, f => Guid.NewGuid().ToString())
        .RuleFor(u => u.Branch, f => f.Company.CompanyName())
        .RuleFor(u => u.CreatedAt, f => DateTime.Now)
        .RuleFor(u => u.Discount, f => new Money(f.Random.Number(1, 50)))
        .RuleFor(u => u.Status, f => f.PickRandom(OrderStatus.Active))
        .RuleFor(u => u.TotalAmount, f => new Money(f.Random.Number(1, 500)));

    private static readonly Faker<Order> OrderInvalidFaker = new Faker<Order>()
        .RuleFor(u => u.Id, f => Guid.NewGuid())
        .RuleFor(u => u.UserId, f => Guid.NewGuid().ToString())
        .RuleFor(u => u.Branch, f => f.Company.CompanyName())
        .RuleFor(u => u.CreatedAt, f => DateTime.Now)
        .RuleFor(u => u.Status, f => f.PickRandom(OrderStatus.Active))
        .RuleFor(u => u.Discount, f => new Money(f.Random.Number(1, 50)))
        .RuleFor(u => u.TotalAmount, f => new Money(f.Random.Number(1, 500)));

    /// <summary>
    /// Generates a valid Order entity with randomized data.
    /// The generated Order will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Order entity with randomly generated data.</returns>
    public static Order GenerateValidOrder()
    {
        return OrderFaker.Generate();
    }

    public static Order GenerateInvalidItemsOrder()
    {
        return OrderInvalidFaker.Generate();
    }
}
