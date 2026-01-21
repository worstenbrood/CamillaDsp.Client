using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CamillaDsp.Client.Models.Config.Mixers
{
    public class Mixer
    {
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("labels")]
        public string[]? Labels { get; set; }

        [JsonPropertyName("channels")]
        public Channels? Channels { get; set; }

        [JsonPropertyName("mapping")]
        public List<Mapping>? Mapping { get; set; }
    }
}
