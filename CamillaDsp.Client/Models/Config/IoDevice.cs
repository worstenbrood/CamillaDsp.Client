using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CamillaDsp.Client.Models.Config
{
    public class IoDevice
    {
        [JsonPropertyName("channels")]
        public int? Channels { get; set; }

        [JsonPropertyName("device")]
        public string? Device { get; set; }

        [JsonPropertyName("format")]
        public string? Format { get; set; }

        [JsonPropertyName("labels")]
        public List<string>? Labels { get; set; }

        [JsonPropertyName("link_mute_control")]
        public string? LinkMuteControl { get; set; }

        [JsonPropertyName("link_volume_control")]
        public string? LinkVolumeControl { get; set; }

        [JsonPropertyName("stop_on_inactive")]
        public bool? StopOnInactive { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }
    }
}
