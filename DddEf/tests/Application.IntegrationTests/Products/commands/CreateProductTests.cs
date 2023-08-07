using DddEf.Application.UseCases.Products.Commands;
using DddEf.Domain.Aggregates.Product;
using DddEf.Domain.Aggregates.Product.ValueObjects;
using FluentAssertions;
using NUnit.Framework;

namespace DddEf.Application.IntegrationTests.Products.commands;

using static Testing;

public class CreateProductTests : BaseTestFixture
{ 
    [Test]
    public async Task ShouldCreateProduct()
    { 
        var command = new CreateProductCommand
        (
            "Code001",
            "Name001"
        );

        var productId = await SendAsync(command);

        var product = await FindAsync<Product>(ProductId.Create(productId));

        product.Should().NotBeNull();
        product!.ProductCode.Should().Be(command.ProductCode);
        product.ProductName.Should().Be(command.ProductName); 
    }
}
