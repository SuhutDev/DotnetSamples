namespace DatetimeOffsetApi.Dto
{
    public class UpdateCustomerReq
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; }
        public DateTimeOffset JoinDate { get; set; }
    }
}
