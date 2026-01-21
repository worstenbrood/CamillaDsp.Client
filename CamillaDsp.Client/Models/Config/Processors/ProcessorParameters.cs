using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CamillaDsp.Client.Models.Config.Processors
{
    public class ProcessorParameters
    {
        [JsonPropertyName("channels")]
        public int? Channels { get; set; }

        [JsonPropertyName("attack")]
        public double? Attack { get; set; }

        [JsonPropertyName("release")]
        public double? Release { get; set; }

        [JsonPropertyName("threshold")]
        public double? Threshold { get; set; }

        [JsonPropertyName("factor")]
        public double? Factor { get; set; }

        [JsonPropertyName("makeup_gain")]
        public double? MakeupGain { get; set; }

        [JsonPropertyName("clip_limit")]
        public double? ClipLimit { get; set; }

        [JsonPropertyName("soft_clip")]
        public bool? SoftClip { get; set; }

        [JsonPropertyName("monitor_channels")]
        public List<int>? MonitorChannels { get; set; }

        [JsonPropertyName("process_channels")]
        public List<int>? ProcessChannels { get; set; }
      
        // NoiseGate-only field in your sample
        [JsonPropertyName("attenuation")]
        public double? Attenuation { get; set; }
    }
}
