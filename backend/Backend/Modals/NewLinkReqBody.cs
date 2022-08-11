using Backend.Enums;
using System.Text.Json.Serialization;

namespace Backend.Modals
{
    public class NewLinkReqBody
    {
        [JsonPropertyName("link")]
        public string? Link { get; set; }

        [JsonPropertyName("slug")]
        public string? Slug { get; set; }

        [JsonPropertyName("exp")]
        public ExpType? Expiration { get; set; }

        [JsonPropertyName("captcha")]
        public string? CaptchaResponse { get; set; }
    }
}