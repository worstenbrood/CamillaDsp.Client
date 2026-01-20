using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CamillaDsp.Client.Models.Config
{
    public class ProcessorParameters
    {
        [JsonPropertyName("attack")]
        public double? Attack { get; set; }

        [JsonPropertyName("channels")]
        public int? Channels { get; set; }

        [JsonPropertyName("clip_limit")]
        public double? ClipLimit { get; set; }

        [JsonPropertyName("factor")]
        public double? Factor { get; set; }

        [JsonPropertyName("makeup_gain")]
        public double? MakeupGain { get; set; }

        [JsonPropertyName("monitor_channels")]
        public List<int>? MonitorChannels { get; set; }

        [JsonPropertyName("process_channels")]
        public List<int>? ProcessChannels { get; set; }

        [JsonPropertyName("release")]
        public double? Release { get; set; }

        [JsonPropertyName("soft_clip")]
        public bool? SoftClip { get; set; }

        [JsonPropertyName("threshold")]
        public double? Threshold { get; set; }

        // NoiseGate-only field in your sample
        [JsonPropertyName("attenuation")]
        public double? Attenuation { get; set; }
    }
}
