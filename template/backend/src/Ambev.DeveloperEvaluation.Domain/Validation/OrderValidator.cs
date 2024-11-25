using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

/// <summary>
/// Validator that defines validation rules for order creation.
/// </summary>
public class OrderValidator : AbstractValidator<Order>
{
    /// <summary>
    /// Initializes a new instance of the CreateOrderCommandValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - UserId: The UserId field is required
    /// - CreatedAt: The CreatedAt field is required
    /// - Branch: The Branch field is required
    /// 
    /// - OrderItems: Order items must be other than empty
    /// - OrderItems: Order items must contain a maximum of 20 items
    /// </remarks>
    public OrderValidator() 
    {
        RuleFor(x => x.UserId).NotNull().NotEmpty().WithMessage("User is required");
        RuleFor(x => x.CreatedAt).NotNull().NotEmpty().WithMessage("Date is required");
        RuleFor(x => x.Branch).NotNull().NotEmpty().Length(3, 100).WithMessage("Branch is required");

        RuleFor(x => x.OrderItems).NotNull().NotEmpty();
        RuleFor(x => x.OrderItems).Must(x => x == null || x.Any());
        RuleFor(x => x.OrderItems).Must(x => x.All(x => !string.IsNullOrWhiteSpace(x.ProductId.ToString())))
                    .WithMessage("Products must not be empty or null.");
        RuleFor(i => i.OrderItems).Must(BeValidChildItemList).WithMessage("Maximum limit 20 items per product");
    }

    /// <summary>
    /// Validate maximum items of order
    /// </summary>
    /// <param name="items">Order Items</param>
    /// <returns></returns>
    private bool BeValidChildItemList(IReadOnlyCollection<OrderItem> items)
    {
        if (items == null || items.Count == 0) return true;

        var totalQuantity = items.GroupBy(gp => gp.ProductId).Select(x => x.Sum(s => s.Quantity));

        foreach (var item in totalQuantity)
        {
            if (item > 20)
                return false;
        }

        return true;
    }
}

