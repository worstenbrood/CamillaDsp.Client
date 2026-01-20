using System.Text.Json.Serialization;

namespace CamillaDsp.Client.Models.Config
{
    public class ResamplerConfig
    {
        [JsonPropertyName("profile")]
        public string? Profile { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }
    }
}
