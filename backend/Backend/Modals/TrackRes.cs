using System.Text.Json.Serialization;

namespace Backend.Modals
{
    public class TrackRes
    {
        [JsonPropertyName("link_info")]
        public LinkInfoDbModal? LinkInfo { get; set; }

        [JsonPropertyName("clicks")]
        public int Clicks { get; set; }

        [JsonPropertyName("by_device")]
        public Dictionary<string, int>? ByDevice { get; set; }

        [JsonPropertyName("by_country")]
        public Dictionary<string, int>? ByCountry { get; set; }

        [JsonPropertyName("by_date")]
        public Dictionary<string, int>? ByDate { get; set; }
    }
}