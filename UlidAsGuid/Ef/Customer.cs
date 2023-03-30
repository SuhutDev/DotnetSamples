using System.ComponentModel;

namespace UlidAsGuid.Ef;

public class Customer
{
    public Ulid Id { get; set; }
    public string CustomerName { get; set; }
}

