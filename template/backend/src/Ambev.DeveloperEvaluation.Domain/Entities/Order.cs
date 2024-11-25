using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.DiscountRating;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Order : BaseEntity
{
    public string UserId { get; private set; } = default!;

    public Money Discount { get; private set; } = default!;

    /// <summary>
    /// Gets the order's current status.
    /// Indicates whether the order is cancelad or active in the system.
    /// </summary>
    public OrderStatus Status { get; private set; } = default!;

    /// <summary>
    /// Gets the date and time when the order was created.
    /// </summary>
    public DateTime CreatedAt { get; private set; } = default!;

    /// <summary>
    /// Gets the date and time of the last update to the order's information.
    /// </summary>
    public DateTime? UpdatedAt { get; private set; } = default!;

    public string Branch { get; private set; } = default!;

    public Money TotalAmount { get; set; }

    private readonly List<OrderItem> _orderItems = new List<OrderItem>();

    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

    public Order() {}

    public Order(Guid id, Money totalAmount)
    {
        Id = id;
        TotalAmount = totalAmount;
        Status = OrderStatus.Active;
        CreatedAt = DateTime.UtcNow;
    }

    public Order(Guid id, List<OrderItem> orderItems)
    {
        Id = id;
        _orderItems.AddRange(orderItems);
        TotalAmount = new Money(0, Currencies.Real.ToString());
        Status = OrderStatus.Active;
        CreatedAt = DateTime.UtcNow;
    }

    public Order(Guid id, string userId, string branchName)
    {
        Id = id;
        UserId = userId;
        Branch = branchName;
        TotalAmount = new Money(0, Currencies.Real.ToString());
        Status = OrderStatus.Active;
        CreatedAt = DateTime.UtcNow;
    }

    public void AddOrderItem(OrderItem orderItem)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(orderItem.Quantity);

        _orderItems.Add(orderItem);

        if (_orderItems.Any(x => x.Quantity >= 10 && x.Quantity <= 20))
            Discount = new BetweenTenAndTwentyItems().CalculateDiscount(OrderItems.Sum(x => x.TotalAmount.Amount));
        else if (_orderItems.Any(x => x.Quantity > 4 && x.Quantity <= 9))
            Discount = new BetweenFourAndNineItems().CalculateDiscount(OrderItems.Sum(x => x.TotalAmount.Amount));
        else
            Discount = new Money(0, Currencies.Real.ToString());

        TotalAmount = new Money(Math.Round(OrderItems.Sum(x => x.TotalAmount.Amount) - Discount.Amount, 2), Currencies.Real.ToString());
    }

    /// <summary>
    /// Cancel order.
    /// Changes the order's status to Cancelad.
    /// </summary>
    public void Cancel()
    {
        Status = OrderStatus.Canceled;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Performs validation of the user entity using the OrderValidator rules.
    /// </summary>
    public ValidationResultDetail Validate()
    {
        var validator = new OrderValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}
