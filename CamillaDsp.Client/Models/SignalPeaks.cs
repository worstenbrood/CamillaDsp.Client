using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
