using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Orders.GetOrder;

public class GetOrderListQueryHandler : IRequestHandler<GetOrderListQuery, List<GetOrderListResult>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public GetOrderListQueryHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<List<GetOrderListResult>> Handle(GetOrderListQuery request, CancellationToken cancellationToken)
    {
        var orderList = await _orderRepository.GetOrders();
        return _mapper.Map<List<GetOrderListResult>>(orderList);
    }
}
