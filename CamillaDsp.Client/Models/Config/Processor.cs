using System.Text.Json.Serialization;

namespace CamillaDsp.Client.Models.Config
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ProcessorTypes
    {
        Compressor,
        NoiseGate
    }

    public class Processor
    {
        [JsonPropertyName("type")]
        public ProcessorTypes? Type { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("parameters")]
        public ProcessorParameters? Parameters { get; set; }
    }
}
