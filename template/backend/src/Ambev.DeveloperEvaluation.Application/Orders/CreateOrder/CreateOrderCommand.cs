using Ambev.DeveloperEvaluation.Application.Orders.Model;
using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Orders.CreateOrder;

public class CreateOrderCommand : IRequest<CreateOrderResult>
{
    /// <summary>
    /// 
    /// </summary>
    public List<OrderItemCommand> OrderItems;

    public string UserId { get; set; }
    public string Date { get; set; }
    public string Branch { get; set; }

    public ValidationResultDetail Validate()
    {
        var validator = new CreateOrderCommandValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}
