using DddEf.Domain.Common.Models;

namespace DddEf.Domain.Aggregates.SalesOrder.ValueObjects;
public sealed class SalesOrderItemDet1Id : ValueObject
{
    public Guid Value { get; }

#pragma warning disable CS8618
    private SalesOrderItemDet1Id()
    {
    }
#pragma warning disable CS8618
    private SalesOrderItemDet1Id(Guid value) => Value = value;

    public static SalesOrderItemDet1Id CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public static SalesOrderItemDet1Id Create(Guid value)
    {
        return new(value);
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}