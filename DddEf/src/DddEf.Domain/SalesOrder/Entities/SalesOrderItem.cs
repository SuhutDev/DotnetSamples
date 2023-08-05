using DddEf.Domain.Common.Models;
using DddEf.Domain.Product.ValueObjects;
using DddEf.Domain.SalesOrder.ValueObjects;

namespace DddEf.Domain.SalesOrder.Entities;

public sealed class SalesOrderItem : EntityDet1<SalesOrderItemDet1Id>
{
#pragma warning disable CS8618
    private SalesOrderItem()
    {

    }
     
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