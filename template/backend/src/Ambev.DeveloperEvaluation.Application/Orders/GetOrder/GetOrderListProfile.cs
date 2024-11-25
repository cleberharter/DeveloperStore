using Ambev.DeveloperEvaluation.Application.Orders.CreateOrder;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Orders.GetOrder;

public class GetOrderListProfile : Profile
{
    public GetOrderListProfile()
    {
        CreateMap<GetOrderListResult, Order>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Branch, opt => opt.MapFrom(src => src.Branch))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.Date))
            .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.Products))
            .ReverseMap();
    }
}
