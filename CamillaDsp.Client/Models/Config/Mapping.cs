using System.Text.Json.Serialization;

namespace CamillaDsp.Client.Models.Config
{
    public class Mapping
    {
        [JsonPropertyName("dest")]
        public int? Destination { get; set; }

        [JsonPropertyName("mute")]
        public bool? Mute { get; set; }

        [JsonPropertyName("sources")]
        public Source[]? Sources { get; set; }
    }
}
