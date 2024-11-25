using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders.CreateOrder;

public class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
{
    public CreateOrderRequestValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().WithMessage("User is required");
        RuleFor(x => x.Date).NotEmpty().WithMessage("Data is required");
        RuleFor(x => x.Branch).NotEmpty().WithMessage("Branch is required").Length(3, 100).WithMessage("Branch name must be between 3 and 100 characters");
        RuleFor(x => x.Products).Must(x => x.All(x => !string.IsNullOrWhiteSpace(x.ProductId)))
                    .WithMessage("Products must not be empty or void.");
    }
}
