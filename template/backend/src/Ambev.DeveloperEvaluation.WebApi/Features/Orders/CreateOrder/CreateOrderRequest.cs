namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders.CreateOrder;

public class CreateOrderRequest
{
    public string UserId { get; set; }
    public string Date { get; set; }
    public string Branch { get; set; }
    public List<OrderItemsProduct> Products { get; set; }
}


public class OrderItemsProduct
{
    public string ProductId { get; set; }
    public int Quantity { get; set; }
}