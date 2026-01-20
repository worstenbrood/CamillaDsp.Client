using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CamillaDsp.Client.Models.Config
{
    public class DspConfig
    {
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("devices")]
        public DeviceConfig? Devices { get; set; }

        /// <summary>
        /// Dynamic object keyed by filter name (e.g. "Unnamed Filter 1")
        /// </summary>
        [JsonPropertyName("filters")]
        public Dictionary<string, FilterDefinition>? Filters { get; set; }

        /// <summary>
        /// Present as an empty object in the sample JSON.
        /// If you later learn the schema, replace object with a concrete type.
        /// </summary>
        [JsonPropertyName("mixers")]
        public Dictionary<string, object?>? Mixers { get; set; }

        [JsonPropertyName("pipeline")]
        public List<Pipeline>? Pipeline { get; set; }

        /// <summary>
        /// Dynamic object keyed by processor name (e.g. "Unnamed Processor 1")
        /// </summary>
        [JsonPropertyName("processors")]
        public Dictionary<string, ProcessorDefinition>? Processors { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }
    }
}
