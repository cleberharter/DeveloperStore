using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Orders.GetOrder;

public class GetOrderListResult
{
    public Guid Id { get; set; }
    public List<OrderItem> Products;
    public string Branch { get; set; }
    public string UserId { get; set; }
    public string Date { get; set; }
}
