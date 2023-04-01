namespace DatetimeOffsetApi.Dto
{
    public class AddCustomerReq
    {
        public string? CustomerName { get; set; }
        public DateTimeOffset JoinDate { get; set; }
    }
}
