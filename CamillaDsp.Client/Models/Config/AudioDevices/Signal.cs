using System.Text.Json.Serialization;

namespace CamillaDsp.Client.Models.Config.AudioDevices
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum SignalTypes
    {
        Sine, 
        Square, 
        WhiteNoise
    }

    public class Signal
    {
        [JsonPropertyName("type")]
        public SignalTypes? Type { get; set; }

        [JsonPropertyName("freq")]
        public int? Frequency { get; set; }

        [JsonPropertyName("level")]
        public float? Level { get; set; }
    }
}
