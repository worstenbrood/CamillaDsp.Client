using System.Text.Json.Serialization;

namespace CamillaDsp.Client.Models
{
    public class Error
    {
        [JsonPropertyName("error")]
        public string? Message { get; set; }    
    }
}
