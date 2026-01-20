using System.Text.Json.Serialization;

namespace CamillaDsp.Client.Models.Config
{
    public class FilterParameters
    {
        [JsonPropertyName("gain")]
        public double? Gain { get; set; }

        [JsonPropertyName("inverted")]
        public bool? Inverted { get; set; }

        [JsonPropertyName("mute")]
        public bool? Mute { get; set; }

        [JsonPropertyName("scale")]
        public string? Scale { get; set; }
    }
}
