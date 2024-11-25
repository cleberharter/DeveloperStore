using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.ValueObjects;

public class Money : ValueObject
{
    public Money(decimal amount, string currency)
    {
        ArgumentNullException.ThrowIfNull(amount);
        ArgumentNullException.ThrowIfNull(currency);
        Amount = amount;
        Currency = currency;
    }
    private Money()
    {
    }
    public Money(decimal amount)
    {
        Amount = amount;
    }

    public decimal Amount { get; set; }
    public string Currency { get; set; }

    public override string ToString() => $"{Amount}{Currency}";
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
    }
}
