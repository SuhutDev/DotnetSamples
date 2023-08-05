using DddEf.Domain.Common.Models;
using DddEf.Domain.Customer.ValueObjects;

namespace DddEf.Domain.Customer;

public sealed class Customer : AggregateRoot<CustomerId>
{
#pragma warning disable CS8618
    private Customer() 
    {

    }

#pragma warning disable CS8618

    private Customer(CustomerId customerId, string customerCode, string customerName)
       : base(customerId)
    {
        CustomerCode = customerCode;
        CustomerName = customerName;
    }
    public static Customer Create(string customerCode, string customerName)
    {
        return new(CustomerId.CreateUnique(), customerCode, customerName);
    }


    public string CustomerCode { get; private set; }
    public string CustomerName { get; private set; }


}