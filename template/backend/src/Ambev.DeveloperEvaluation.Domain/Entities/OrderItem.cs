using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class OrderItem : BaseEntity
{
    public Order Order { get; private set; }
    public Guid OrderId { get; private set; }
    public Product Product { get; private set; }
    public Guid ProductId { get; private set; }
    public int Quantity { get; private set; }
    public Money TotalAmount { get; private set; }

    public OrderItem()
    {
        
    }

    public OrderItem(Guid orderId, Guid productId, int quantity, Money totalAmount)
    {
        OrderId = orderId;
        ProductId = productId;
        Quantity = quantity;
        TotalAmount = totalAmount;
    }
}
