using DddEf.Domain.Aggregates.Customer.ValueObjects;
using DddEf.Domain.Aggregates.SalesOrder.Entities;
using DddEf.Domain.Aggregates.SalesOrder.ValueObjects;
using DddEf.Domain.Common.Models;

namespace DddEf.Domain.Aggregates.SalesOrder;
public sealed class SalesOrder : AggregateRoot<SalesOrderId>
{
#pragma warning disable CS8618
    private SalesOrder()
    {

    } 
#pragma warning disable CS8618
    public string TransNo { get; private set; }
    public DateTime TransDate { get; private set; }
    public CustomerId CustomerId { get; private set; }
    public IReadOnlyList<SalesOrderItem> Items => _items.AsReadOnly();

    private readonly List<SalesOrderItem> _items = new();

    private SalesOrder(SalesOrderId id, string transNo, DateTime transDate, CustomerId customerId,
        List<SalesOrderItem> items)
        : base(id)
    {
        TransNo = transNo;
        TransDate = transDate;
        CustomerId = customerId;
        _items = items;
    }


    public static SalesOrder Create(string transNo, DateTime transDate, CustomerId customerId,
                              List<SalesOrderItem> items)
    {
        return new(SalesOrderId.CreateUnique(), transNo, transDate, customerId, items);
    }
}