using Ambev.DeveloperEvaluation.Application.Orders.CreateOrder;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders.CreateOrder;

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
        CreateMap<CreateOrderResult, CreateOrderResponse>();
    }
}
