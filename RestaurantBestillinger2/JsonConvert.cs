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

        public static string CheckAndCreateReservationsJson(string args2, string expectedJsonFile)  // how do I make this work?? return filename?
        {
            if (args2 != expectedJsonFile)
            {
                List<Reservation> reservations = new List<Reservation>();
                string jsonString = JsonSerializer.Serialize(reservations);
                File.WriteAllText(@$"C:\Users\Mayuko\source\repos\RestaurantBestillinger2\RestaurantBestillinger2\{expectedJsonFile}", jsonString);
                return expectedJsonFile;
            }
            else return args2;
        }
    }
}