using Newtonsoft.Json;

namespace Utils
{
    public static class DataFormatter
    {
        static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings()
        { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
        
        public static string Serialize<T>(T value)
        {
            if (value == null)
                return null;
            return JsonConvert.SerializeObject(value, SerializerSettings);
        }

        public static T Deserialize<T>(string json)
        {
            if (string.IsNullOrEmpty(json))
                return default;
            return JsonConvert.DeserializeObject<T>(json, SerializerSettings);
        }
    }
}