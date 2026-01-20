using System.Text.Json.Serialization;

namespace CamillaDsp.Client.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum StopReason
    {
        None,
        Done,
        CaptureError,
        PlaybackError,
        CaptureFormatChange,
        PlaybackFormatChange
    }
}
