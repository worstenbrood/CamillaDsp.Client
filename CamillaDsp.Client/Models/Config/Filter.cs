using System.Text.Json.Serialization;

namespace CamillaDsp.Client.Models.Config
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum FilterTypes
    {
        Volume,
        Gain,
        Loudness,
        Delay,
        Biquad, 
        BiquadCombo,
        DiffEq,
        Conv
    }

    public class Filter
    {
        [JsonPropertyName("type")]
        public FilterTypes? Type { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("parameters")]
        public FilterParameters? Parameters { get; set; }
    }
}
