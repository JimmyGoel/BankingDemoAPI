using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace ApplicationCore.Extensions
{
    public static class JsonExtension
    {
        public static readonly JsonSerializerOptions serializerOptions = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        };

        public static T FromJson<T>(this string json) =>
            JsonSerializer.Deserialize<T>(json, serializerOptions);
        public static string ToJson<T>(this T obj) =>
            JsonSerializer.Serialize<T>(obj, serializerOptions);
    }
}

