using System.Text.Json.Serialization;

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
