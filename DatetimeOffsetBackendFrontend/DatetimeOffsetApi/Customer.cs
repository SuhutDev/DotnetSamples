namespace DatetimeOffsetApi;

public class Customer
{
    public Guid Id { get; set; }
    public string? CustomerName { get; set; }
    public DateTimeOffset JoinDate { get; set; }
}
