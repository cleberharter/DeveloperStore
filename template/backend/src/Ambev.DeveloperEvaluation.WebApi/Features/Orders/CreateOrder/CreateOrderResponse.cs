using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders.CreateOrder
{
    public class CreateOrderResponse
    {

        public Guid Id { get; set; }
        public string Branch { get; set; }
        public string UserId { get; set; }
        public string Date { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Discount { get; set; }
        public List<OrderItem> Products;
    }
}
