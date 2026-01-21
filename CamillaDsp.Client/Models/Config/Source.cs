using System.Text.Json.Serialization;

namespace CamillaDsp.Client.Models.Config
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ScaleTypes
    {
        dB,
        linear
    }

    public class Source
    {
        [JsonPropertyName("channel")]
        public int? Channel { get; set; }

        [JsonPropertyName("gain")]
        public float? Gain { get; set; }

        [JsonPropertyName("inverted")]
        public bool? Inverted { get; set; }

        [JsonPropertyName("scale")]
        public ScaleTypes? Scale { get; set; }
    }
}
