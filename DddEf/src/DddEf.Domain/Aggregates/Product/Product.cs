using DddEf.Domain.Aggregates.Product.ValueObjects;
using DddEf.Domain.Common.Models;

namespace DddEf.Domain.Aggregates.Product;

public sealed class Product : AggregateRoot<ProductId>
{
#pragma warning disable CS8618
    private Product()
    {

    }

#pragma warning disable CS8618
    public string ProductCode { get; private set; }
    public string ProductName { get; private set; }

    private Product(ProductId id, string productCode, string productName)
        : base(id)
    {
        ProductCode = productCode;
        ProductName = productName;
    }

    public static Product Create(string productCode, string productName)
    {
        return new(ProductId.CreateUnique(), productCode, productName);
    }
}