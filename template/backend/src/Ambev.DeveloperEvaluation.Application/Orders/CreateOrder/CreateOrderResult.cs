using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Orders.CreateOrder;

/// <summary>
/// Represents the response returned after successfully creating a new order.
/// </summary>
/// <remarks>
/// This response contains the unique identifier of the newly created order,
/// which can be used for subsequent operations or reference.
/// </remarks>
public class CreateOrderResult
{
    public Guid Id { get; set; }
    public string Branch { get; set; }
    public string UserId { get; set; }
    public string Date { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal Discount { get; set; }
    public List<OrderItem> Products;
}
