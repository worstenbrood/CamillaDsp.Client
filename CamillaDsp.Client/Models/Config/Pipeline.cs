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
        [JsonPropertyName("bypassed")]
        public bool? Bypassed { get; set; }

        [JsonPropertyName("channels")]
        public int[]? Channels { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("names")]
        public string[]? Names { get; set; }

        [JsonPropertyName("type")]
        public PipelineTypes? Type { get; set; }
    }
}
