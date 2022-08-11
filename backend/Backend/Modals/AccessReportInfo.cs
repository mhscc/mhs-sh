namespace Backend.Modals
{
    public class AccessReportInfo
    {
        public string? Id { get; set; }

        public string? UserAgent { get; set; }

        public string? IpAddress { get; set; }

        public DateTime AccessedAt { get; private set; } = DateTime.UtcNow;
    }
}