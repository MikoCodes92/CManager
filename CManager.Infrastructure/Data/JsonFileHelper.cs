using System.Text.Json;
using System.Text.Json.Serialization;

namespace CManager.Infrastructure.Data
{
    public static class JsonFileHelper
    {
        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            WriteIndented = true,
            Converters =
            {
                new JsonStringEnumConverter()
            },
            PropertyNameCaseInsensitive = true
        };
        public static List<T> ReadFromJsonFile<T>(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    return new List<T>();
                }
                var jsonString = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<List<T>>(jsonString, _jsonOptions) ?? new List<T>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading JSON file: {ex.Message}");
                return new List<T>();
            }
        }

        public static void WriteToJsonFile<T>(string filePath, List<T> data)
        {
            try
            {
                var jsonString = JsonSerializer.Serialize(data, _jsonOptions);
                File.WriteAllText(filePath, jsonString);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing JSON file: {ex.Message}");
            }
        }
    }
}
