using Newtonsoft.Json;

namespace DualTracking.Api
{
    public class Token
    {
        [JsonProperty("access_token")]
        public string? AccessToken { get; set; }

        [JsonProperty("token_type")]
        public string? TokenType { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("userName")]
        public string? Username { get; set; }

        [JsonProperty(".issued")]
        public string? IssuedAt { get; set; }

        [JsonProperty(".expires")]
        public string? ExpiresAt { get; set; }
    }
}
