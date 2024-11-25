using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Orders.GetOrder;

public class GetOrderListQuery : IRequest<List<GetOrderListResult>>
{
    public GetOrderListQuery()
    {
        
    }
}
