using System.Text.Json.Serialization;

namespace CamillaDsp.Client.Models
{
    public class Signals
    {
        [JsonPropertyName("playback_peak")]
        public float[]? PlaybackPeak { get; set; }

        [JsonPropertyName("playback_rms")]
        public float[]? PlaybackRms { get; set; }

        [JsonPropertyName("capture_peak")]
        public float[]? CapturePeak { get; set; }

        [JsonPropertyName("capture_rms")]
        public float[]? CaptureRms { get; set; }
    }
}
