namespace Ambev.DeveloperEvaluation.Application.Orders.Model;

public class OrderItemCommand
{
    public Guid ProductId { get; init; }
    public int Quantity { get; init; }

    public OrderItemCommand() { }

    public OrderItemCommand(Guid productId, int quantity)
    {
        ProductId = productId;
        Quantity = quantity;
    }
}
