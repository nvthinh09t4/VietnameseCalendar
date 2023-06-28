namespace webapi.Entities
{
    public class TrackingInfor : BaseEntity
    {
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }
        public string? AdditionInfor { get; set; }
    }
}
