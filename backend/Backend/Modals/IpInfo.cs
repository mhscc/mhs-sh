using System.Text.Json.Serialization;

namespace Backend.Modals
{
    public class IpInfo
    {
        [JsonPropertyName("countryCode")]
        public string? CountryCode { get; set; }
    }
}