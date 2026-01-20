using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CamillaDsp.Client.Models.Config
{
    public class FilterDefinition
    {
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("parameters")]
        public FilterParameters? Parameters { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }
    }
}
