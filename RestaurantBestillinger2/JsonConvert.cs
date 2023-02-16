using System.Text.Json;

namespace RestaurantBestillinger2
{
    internal class JsonConvert
    {
        public static T Load<T>(string fileName)
        {
            var json = File.ReadAllText(fileName);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };            
            return JsonSerializer.Deserialize<T>(json, options);
        }
    }
}
