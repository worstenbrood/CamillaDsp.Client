using System.Text.Json.Serialization;

namespace CamillaDsp.Client.Models.Config
{
    public class Channels
    {
        [JsonPropertyName("in")]
        public int? In { get; set; }

        [JsonPropertyName("out")]
        public int? Out { get; set; }
    }
}
