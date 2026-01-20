using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CamillaDsp.Client.Models.Config
{
    public class FilterParameters
    {
        [JsonPropertyName("gain")]
        public double? Gain { get; set; }

        [JsonPropertyName("inverted")]
        public bool? Inverted { get; set; }

        [JsonPropertyName("mute")]
        public bool? Mute { get; set; }

        [JsonPropertyName("scale")]
        public string? Scale { get; set; }
    }
}
