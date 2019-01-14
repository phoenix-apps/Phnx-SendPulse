using Newtonsoft.Json;

namespace Phnx.SendPulse.Models
{
    public class SendPulseLoginRequestModel
    {
        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        [JsonProperty("client_secret")]
        public string ClientSecret { get; set; }

        [JsonProperty("grant_type")]
        public string GrantType { get; set; }
    }
}