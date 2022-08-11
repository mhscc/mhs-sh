using System.Text.Json.Serialization;

namespace Backend.Modals
{
    public class ReCaptchaResp
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("hostname")]
        public string? Host { get; set; }

        [JsonPropertyName("error-codes")]
        public string[]? Errors { get; set; }
    }
}