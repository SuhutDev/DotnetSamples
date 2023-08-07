using DddEf.Domain.Common.Models;

namespace DddEf.Domain.Aggregates.Product.ValueObjects;

public sealed class ProductId : ValueObject
{
    public Guid Value { get; }
#pragma warning disable CS8618
    private ProductId()
    {
    }
#pragma warning disable CS8618

    private ProductId(Guid value) => Value = value;

    public static ProductId CreateUnique()
    {
        return new(Guid.NewGuid());
    }
    public static ProductId Create(Guid value)
    {
        return new(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}