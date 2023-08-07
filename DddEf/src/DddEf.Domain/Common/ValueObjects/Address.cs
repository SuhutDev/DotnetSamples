using DddEf.Domain.Common.Models;

namespace DddEf.Domain.Common.ValueObjects;

public class Address : ValueObject
{
#pragma warning disable CS8618
    private Address()
    {
    }
#pragma warning disable CS8618
    public Address(string city, string country)
    {
        City = city;
        Country = country;
    }

    public string City { get; }

    public string Country { get; }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return City;
        yield return Country;
    }
}
