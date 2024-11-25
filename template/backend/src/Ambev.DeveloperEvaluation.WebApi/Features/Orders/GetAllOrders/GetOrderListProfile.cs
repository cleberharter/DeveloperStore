using Ambev.DeveloperEvaluation.Application.Orders.CreateOrder;
using Ambev.DeveloperEvaluation.Application.Orders.GetOrder;
using Ambev.DeveloperEvaluation.WebApi.Features.Orders.CreateOrder;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders.GetAllOrders;

public class GetOrderListProfile : Profile
{
    public GetOrderListProfile()
    {
        CreateMap<GetOrderListResult, GetOrderListResponse>().ReverseMap();
    }
}
