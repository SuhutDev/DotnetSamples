using DddEf.Domain.Aggregates.Customer.ValueObjects;
using DddEf.Domain.Aggregates.SalesOrder.Entities;
using DddEf.Domain.Aggregates.SalesOrder.ValueObjects;
using DddEf.Domain.Common.Models;
using DddEf.Domain.Common.ValueObjects;

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
    public Address ShipAddress { get; private set; }
    public Address BillAddress { get; private set; }
    public IReadOnlyList<SalesOrderItem> Items => _items.AsReadOnly();

    private readonly List<SalesOrderItem> _items = new();
    public double Total { get; private set; }

    private SalesOrder(SalesOrderId id, string transNo, DateTime transDate, CustomerId customerId,
        Address billAddress,
        Address shipAddress,
        List<SalesOrderItem> items)
        : base(id)
    {
        TransNo = transNo;
        TransDate = transDate;
        CustomerId = customerId;
        BillAddress = billAddress;
        ShipAddress = shipAddress;
        _items = items;
        Total = items.Sum(p => p.Total ?? 0);
    }


    public static SalesOrder Create(string transNo, DateTime transDate, CustomerId customerId,
                                Address billAddress,
                                Address shipAddress,
                              List<SalesOrderItem> items)
    {
        return new(SalesOrderId.CreateUnique(), transNo, transDate, customerId, billAddress, shipAddress, items);
    }
}