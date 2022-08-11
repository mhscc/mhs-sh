using System.Text.Json.Serialization;

namespace Backend.Modals
{
    public class ErrorResBody
    {
        [JsonPropertyName("error")]
        public string? Message { get; set; }
    }
}