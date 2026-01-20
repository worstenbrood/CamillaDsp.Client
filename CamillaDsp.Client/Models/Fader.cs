using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CamillaDsp.Client.Models
{
    public class Fader
    {
        [JsonPropertyName("volume")]
        public float? Volume { get; set; }

        [JsonPropertyName("mute")]
        public bool? Mute { get; set; }
    }
}
