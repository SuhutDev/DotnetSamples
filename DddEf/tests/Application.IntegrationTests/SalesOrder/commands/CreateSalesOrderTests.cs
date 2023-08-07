using DddEf.Application.UseCases.Customers.Commands;
using DddEf.Application.UseCases.Products.Commands;
using DddEf.Application.UseCases.SalesOrders.Commands;
using DddEf.Domain.Aggregates.Customer.ValueObjects;
using DddEf.Domain.Aggregates.Product.ValueObjects;
using DddEf.Domain.Aggregates.SalesOrder;
using DddEf.Domain.Aggregates.SalesOrder.ValueObjects;
using FluentAssertions;
using NUnit.Framework;

namespace DddEf.Application.IntegrationTests.Customers.commands;

using static Testing;

public class CreateSalesOrderTests : BaseTestFixture
{ 
    [Test]
    public async Task ShouldCreateCustomer()
    { 
        var createCustomerCommand = new CreateCustomerCommand
        (
            "CustomerCode01",
            "CustomerName01"
        ); 
        var customerId = await SendAsync(createCustomerCommand);

        var createProductCommand1 = new CreateProductCommand
        (
            "ProductCode001",
            "ProductName001"
        );
        var productId1 = await SendAsync(createProductCommand1);

        var createProductCommand2 = new CreateProductCommand
        (
            "ProductCode002",
            "ProductName002"
        );
        var productId2 = await SendAsync(createProductCommand2);

        var createSalesOrderCommand = new CreateSalesOrderCommand
        (
            "Trans001",
            DateTime.Now.Date,
            CustomerId.Create(customerId),
            new List<SalesOrderItemVm>
            {
                new SalesOrderItemVm(ProductId.Create(productId1),1,1000),
                new SalesOrderItemVm(ProductId.Create(productId2),2,2000)
            }
        );

        var salesOrderId = await SendAsync(createSalesOrderCommand);

        var salesOrder = await FindAsync<SalesOrder>(SalesOrderId.Create(salesOrderId));

        salesOrder.Should().NotBeNull();
        salesOrder!.TransNo.Should().Be(createSalesOrderCommand.TransNo); 
        salesOrder.TransDate.Should().Be(createSalesOrderCommand.TransDate);
        salesOrder.Items.Should().NotBeNull();
        salesOrder.Items.Count.Should().Be(createSalesOrderCommand.Items.Count);
    }
}
