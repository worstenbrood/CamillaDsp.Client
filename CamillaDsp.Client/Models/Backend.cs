using System.Text.Json.Serialization;

namespace CamillaDsp.Client.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Backend
    {
        Alsa, 
        CoreAudio,
        Wasapi
    }
}
