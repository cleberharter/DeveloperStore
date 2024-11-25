using Ambev.DeveloperEvaluation.Application.Orders.Model;
using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Orders.CreateOrder;

/// <summary>
/// Profile for mapping between Application and API CreateOrder responses
/// </summary>
public class CreateOrderProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateOrder feature
    /// </summary>
    public CreateOrderProfile()
    {
        CreateMap<CreateOrderResult, Order>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Branch, opt => opt.MapFrom(src => src.Branch))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.Date))
            .ForPath(dest => dest.Discount.Amount, opt => opt.MapFrom(src => src.Discount))
            .ForPath(dest => dest.TotalAmount.Amount, opt => opt.MapFrom(src => src.TotalAmount))
            .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.Products))
            .ReverseMap();
    }
}
