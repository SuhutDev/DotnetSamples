using DddEf.Domain.Common.Models;
using DddEf.Domain.Product.ValueObjects;

namespace DddEf.Domain.Product.Entities;

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