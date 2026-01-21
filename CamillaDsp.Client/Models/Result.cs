using System.Text.Json.Serialization;

namespace CamillaDsp.Client.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Result
    {
        Ok,
        Error
    }

    public class Result<T>
    {
        [JsonPropertyName("result")]
        public Result? Status { get; set; }

        [JsonPropertyName("value")]
        public T? Value { get; set; }
    }
}
