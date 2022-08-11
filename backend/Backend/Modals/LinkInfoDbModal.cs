using Backend.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace Backend.Modals
{
    public class LinkInfoDbModal
    {
        [BsonId]
        [BsonRequired]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonPropertyName("id")]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        [BsonRequired]
        [BsonElement("tracking_id")]
        [JsonPropertyName("tracking_id")]
        public string? TrackingId { get; set; } = Guid.NewGuid().ToString();

        [BsonRequired]
        [BsonElement("link")]
        [JsonPropertyName("link")]
        public string? Link { get; set; }

        [BsonRequired]
        [BsonElement("slug")]
        [JsonPropertyName("slug")]
        public string? Slug { get; set; }

        [BsonElement("exp")]
        [JsonPropertyName("exp")]
        public ExpType? Expiration { get; set; }

        [BsonElement("exp_raw")]
        [JsonPropertyName("exp_raw")]
        public DateTime? ExpirationRaw { get; set; }

        [BsonElement("created_at")]
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        [JsonIgnore]
        [BsonRequired]
        [BsonElement("creator")]
        public CreatorInfo? Creator { get; set; }

        [JsonIgnore]
        [BsonElement("clicks")]
        public List<UserAccessInfo>? Clicks { get; set; }

        [BsonIgnore]
        [JsonPropertyName("prev_created")]
        public bool? PreviouslyCreated { get; set; } = null;
    }

    public class CreatorInfo
    {
        [BsonElement("ip_addr")]
        public string? IpAddress { get; set; }

        [BsonElement("user_agent")]
        public string? UserAgent { get; set; }
    }

    public class UserAccessInfo
    {
        [BsonElement("device")]
        [JsonPropertyName("device")]
        public string? Device { get; set; }

        [BsonElement("country")]
        [JsonPropertyName("country")]
        public string? Country { get; set; }

        [BsonElement("accessed_at")]
        [JsonPropertyName("accessed_at")]
        public DateTime AccessedAt { get; set; }
    }
}