using System.Text.Json.Serialization;

namespace CamillaDsp.Client.Models.Config.AudioDevices
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ResamplerTypes
    {
        AsyncSinc,
        AsyncPoly,
        Synchronous,
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum InterpolationTypes
    {
        Nearest,
        Linear,
        Quadratic,
        Cubic,
        Quintic,
        Septic
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum WindowTypes
    {
        Blackman,
        Blackman2,
        BlackmanHarris,
        BlackmanHarris2,
        Hann,
        Hann2,
    }

    public class Resampler
    {
        [JsonPropertyName("type")]
        public ResamplerTypes? Type { get; set; }

        [JsonPropertyName("profile")]
        public string? Profile { get; set; }

        [JsonPropertyName("sinc_len")]
        public int? SincLength{ get; set; }

        [JsonPropertyName("oversampling_factor")]
        public int? OversamplingFactor { get; set; }

        [JsonPropertyName("interpolation")]
        public InterpolationTypes? Interpolation { get; set; }

        [JsonPropertyName("window")]
        public WindowTypes? Window { get; set; }

        [JsonPropertyName("f_cutoff")] 
        public float? Cutoff { get; set; }
    }
}
