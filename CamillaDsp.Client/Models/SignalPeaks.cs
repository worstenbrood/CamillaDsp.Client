using System.Text.Json.Serialization;

namespace CamillaDsp.Client.Models
{
    public class SignalPeaks
    {
        [JsonPropertyName("playback")]
        public float[]? Playback { get; set; }

        [JsonPropertyName("capture")]
        public float[]? Capture { get; set; }
    }
}
