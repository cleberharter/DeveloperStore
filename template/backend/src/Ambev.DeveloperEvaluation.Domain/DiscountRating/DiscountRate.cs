using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Domain.DiscountRating;

public abstract class DiscountRate
{
    protected abstract decimal PercentOfDiscountRate { get; }

    public Money CalculateDiscount(decimal total)
    {
        return new Money((total / 100) * PercentOfDiscountRate, Currencies.Real.ToString());
    }
}
