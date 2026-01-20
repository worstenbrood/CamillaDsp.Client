using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CamillaDsp.Client.Models.Config
{
    public class Pipeline
    {
        [JsonPropertyName("bypassed")]
        public bool? Bypassed { get; set; }

        [JsonPropertyName("channels")]
        public List<int>? Channels { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("names")]
        public List<string>? Names { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }
    }
}
