using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CamillaDsp.Client.Models.Config
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PipelineTypes
    {
        Processor,
        Filter,
        Mixer,
    }

    public class Pipeline
    {
        [JsonPropertyName("type")]
        public PipelineTypes? Type { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("channels")]
        public List<int>? Channels { get; set; }

        [JsonPropertyName("bypassed")]
        public bool? Bypassed { get; set; }
       
        [JsonPropertyName("names")]
        public List<string>? Names { get; set; }
    }
}
