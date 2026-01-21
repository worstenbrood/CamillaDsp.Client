using CamillaDsp.Client.Base;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CamillaDsp.Client.Models.Config
{
    public class DspConfig
    {
        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("devices")]
        public Devices? Devices { get; set; }

        /// <summary>
        /// Dynamic object keyed by filter name (e.g. "Unnamed Filter 1")
        /// </summary>
        [JsonPropertyName("filters")]
        public Dictionary<string, Filter>? Filters { get; set; }

        /// <summary>
        /// Present as an empty object in the sample JSON.
        /// </summary>
        [JsonPropertyName("mixers")]
        public Dictionary<string, Mixer>? Mixers { get; set; }

        /// <summary>
        /// Dynamic object keyed by processor name (e.g. "Unnamed Processor 1")
        /// </summary>
        [JsonPropertyName("processors")]
        public Dictionary<string, Processor>? Processors { get; set; }

        /// <summary>
        /// Pipelines
        /// </summary>
        [JsonPropertyName("pipeline")]
        public List<Pipeline>? Pipeline { get; set; }

        /// <summary>
        /// Return this object serialized as a JSON string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Serializer.Serialize(this);
        }
    }
}
