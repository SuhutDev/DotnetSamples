using DddEf.Domain.Common.Models;

namespace DddEf.Domain.Customer.ValueObjects;

public sealed class CustomerId : ValueObject
{
    public Guid Value { get; }
#pragma warning disable CS8618
    private CustomerId()
    {
    }
#pragma warning disable CS8618

    private CustomerId(Guid value) => Value = value;

    public static CustomerId CreateUnique()
    {
        return new(Guid.NewGuid());
    }
    public static CustomerId Create(Guid value)
    {
        return new(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

}