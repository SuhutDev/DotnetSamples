using DddEf.Domain.Common.Models;

namespace DddEf.Domain.Aggregates.SalesOrder.ValueObjects;
public sealed class SalesOrderId : ValueObject
{
    public Guid Value { get; }
#pragma warning disable CS8618
    private SalesOrderId()
    {
    }
#pragma warning disable CS8618

    private SalesOrderId(Guid value) => Value = value;

    public static SalesOrderId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public static SalesOrderId Create(Guid value)
    {
        return new(value);
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}