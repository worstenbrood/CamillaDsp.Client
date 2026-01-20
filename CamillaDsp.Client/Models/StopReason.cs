using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
