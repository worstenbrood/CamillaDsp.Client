using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CamillaDsp.Client.Base
{
    public static class Serializer
    {
        public static readonly JsonSerializerOptions JsonSerializerOptions = new()
        {
            WriteIndented = false,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        };

        public static string Serialize<T>(T obj)
        {
            return JsonSerializer.Serialize(obj, JsonSerializerOptions);
        }

        public static T? Deserialize<T>(string json)
        {
            return JsonSerializer.Deserialize<T>(json, JsonSerializerOptions);
        }
    }
}
