using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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
