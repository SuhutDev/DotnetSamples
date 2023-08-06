namespace Application.FunctionalTests.Customers.Commands;

using DddEf.Application.UseCases.Customers.Commands;
using DddEf.Domain.Aggregates.Customer;
using FluentAssertions;

using NUnit.Framework;

using static Testing;

public class CreateCustomerCommandTests : BaseTestFixture
{
    [Test]
    public async Task ShouldCreateCustomer()
    {
        var command = new CreateCustomerCommand
        (
            "Code001",
            "Name001"
        );

        var itemId = await SendAsync(command);

        var item = await FindAsync<Customer>(itemId);

        item.Should().NotBeNull();
        item!.CustomerCode.Should().Be(command.CustomerCode);
        item.CustomerName.Should().Be(command.CustomerName);
    }
}


