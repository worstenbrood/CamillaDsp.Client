using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CamillaDsp.Client.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum CDSPResultStatus
    {
        Ok,
        Error
    }

    public class CDSPResult<T>
    {
        [JsonPropertyName("result")]
        public CDSPResultStatus? Result { get; set; }

        [JsonPropertyName("value")]
        public T? Value { get; set; }
    }
}
