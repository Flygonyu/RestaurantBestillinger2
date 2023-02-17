using System.Text.Json;

namespace RestaurantBestillinger2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            args[1] = JsonConvert.CheckAndCreateReservationsJson(args[1], "reservations.json");
            RestaurantApp restaurant = new(args[0], args[1]);

            var hours = args[2];
            var minutes = args[3];

            if (args.Length > 4)
            {
                var customerCount = Convert.ToInt32(args[4]);
                var customerName = args.Length > 5 ? args[5] : "Ukjent";
                var customerPhone = args.Length > 6 ? args[6] : "Ukjent";

                restaurant.BookReservation(hours, minutes, customerCount, customerName, customerPhone);
            }

            else if (args.Length == 4) restaurant.GetReservations(hours, minutes);
            else Console.WriteLine("Error: ikke nok informasjon");
        }
    }
}