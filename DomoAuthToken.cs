using System;
using System.Text.Json.Serialization;

namespace OnCourse.Domo
{
    public class DomoAuthToken
    {
        [JsonPropertyName("access_token")]
        public string Token { get; set; }
        [JsonPropertyName("token_type")]
        public string Token_type { get; set; }
        [JsonPropertyName("expires_in")]
        public int Expires_in { get; set; } // Seconds?
        [JsonPropertyName("scope")]
        public string Scope { get; set; }
        [JsonPropertyName("customer")]
        public string Customer { get; set; }
        [JsonPropertyName("env")]
        public string Env { get; set; }
        [JsonPropertyName("userId")]
        public int UserId { get; set; }
        [JsonPropertyName("role")]
        public string Role { get; set; }
        [JsonPropertyName("jti")]
        public string Jti { get; set; }

        [JsonIgnore]
        public DateTime NeedNewTokenAt => NeedToGetNewToken();
        private DateTime NeedToGetNewToken()
        {
            var timeToGetNewToken = DateTime.Now;
            timeToGetNewToken.AddSeconds(Expires_in);
            return timeToGetNewToken;
        }
    }
}