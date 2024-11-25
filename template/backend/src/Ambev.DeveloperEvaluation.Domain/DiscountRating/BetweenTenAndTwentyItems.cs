namespace Ambev.DeveloperEvaluation.Domain.DiscountRating;

public class BetweenTenAndTwentyItems : DiscountRate
{
    protected override decimal PercentOfDiscountRate => 20;
}
