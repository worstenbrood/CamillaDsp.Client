using System.Text.Json.Serialization;

namespace CamillaDsp.Client.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum State
    {
        Running,
        Paused,
        Inactive,
        Starting,
        Stalled
    }
}
