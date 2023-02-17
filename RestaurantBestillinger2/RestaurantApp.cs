using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RestaurantBestillinger2
{
    internal class RestaurantApp
    {
        public List<Reservation> Reservations { get; private set; }
        public Table[] Tables { get; private set; }

        public RestaurantApp(string tablesFile, string reservationsFile)
        {
            Tables = JsonConvert.Load<Table[]>(tablesFile);
            Reservations = JsonConvert.Load<List<Reservation>>(reservationsFile);
        }

        public void GetReservations(string hours, string minutes)
        {
            Console.WriteLine($"Reservasjoner for kl.{hours}:{minutes}:\n");
            FindAndShowReservation(hours, minutes, Reservations);
        }

        private void FindAndShowReservation(string hours, string minutes, List<Reservation> reservations)
        {
            foreach (var reservation in reservations)
            {
                if (IsWithinTimeFrame(hours, minutes, reservation)) ShowReservationInfo(reservation);
            }
        }

        private void ShowReservationInfo(Reservation reservation)
        {
            Console.WriteLine($"Bord: {reservation.Table}\n" +
                              $"Navn: {reservation.CustomerName}\n" +
                              $"Tlf: {reservation.CustomerPhone}\n" +
                              $"Antall: {reservation.Count}\n" +
                              $"Reservert fra {reservation.StartTime} til {reservation.StartTime + 200}");
            Console.WriteLine();
        }

        public void BookReservation(string hours, string minutes, int customerCount, string customerName, string customerPhone)
        {
            var table = FindFreeTableWithCapacity(hours, minutes, customerCount);
            if (table != null)
            {
                var newReservation = new Reservation()
                {
                    Count = Convert.ToInt32(customerCount),
                    CustomerName = customerName,
                    CustomerPhone = customerPhone,
                    StartTime = Convert.ToInt32(hours + minutes),
                    Table = table.Name
                };
                Reservations.Add(newReservation);
                Console.WriteLine($"{customerName} booket bord {table.Name} for {customerCount} personer");
                return;
            }
            Console.WriteLine("Ingen bord tilgjengelig");
        }

        private Table FindFreeTableWithCapacity(string hours, string minutes, int customerCount)
        {
            var tablesByCapacity = Tables.OrderBy(t => t.Capacity);
            foreach (var table in tablesByCapacity)
            {
                if (Convert.ToInt32(customerCount) <= table.Capacity && TableIsFree(hours, minutes, table.Name) == true)
                {
                    return table;
                }
            }
            return null;
        }

        private bool TableIsFree(string hours, string minutes, string tableName)
        {
            foreach (var reservation in Reservations)
            {
                if (reservation.Table.Contains(tableName) && IsWithinTimeFrame(hours, minutes, reservation))
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsWithinTimeFrame(string hours, string minutes, Reservation reservation)
        {
            int searchedTime = Convert.ToInt32(hours + minutes);
            int reservationEndTime = reservation.StartTime + 200;

            if (reservation.StartTime <= searchedTime && reservationEndTime >= searchedTime) return true;
            else return false;
        }
    }
}
