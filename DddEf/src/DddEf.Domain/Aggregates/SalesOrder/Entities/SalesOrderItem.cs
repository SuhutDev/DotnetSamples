using DddEf.Domain.Aggregates.Product.ValueObjects;
using DddEf.Domain.Aggregates.SalesOrder.ValueObjects;
using DddEf.Domain.Common.Models;

namespace DddEf.Domain.Aggregates.SalesOrder.Entities;

public sealed class SalesOrderItem : EntityDet1<SalesOrderItemDet1Id>
{
#pragma warning disable CS8618
    private SalesOrderItem()
    {

    } 
    
    public SalesOrderId Id { get; }
#pragma warning disable CS8618
    public int RowNumber { get; private set; }
    public ProductId ProductId { get; private set; }
    public double? Qty { get; private set; }
    public double? Price { get; private set; }
    public SalesOrderItem(
                        SalesOrderItemDet1Id detId,
                        int rowNumber,
                        ProductId productId,
                       double qty,
                       double price
       )
     : base(detId)
    {
        RowNumber = rowNumber;
        ProductId = productId;
        Qty = qty;
        Price = price;
    }
    public static SalesOrderItem Create(
                        int rowNumber,
                        ProductId productId,
                       double qty,
                       double price)
    {
        return new(SalesOrderItemDet1Id.CreateUnique(), rowNumber, productId, qty, price);
    }
}