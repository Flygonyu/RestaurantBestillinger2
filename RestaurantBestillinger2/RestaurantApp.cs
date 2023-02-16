﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBestillinger2
{
    internal class RestaurantApp
    {
        public Reservation[] Reservations { get; private set; }
        public Table[] Tables { get; private set; }

        public RestaurantApp(string tablesFile, string reservationsFile)
        {
            Tables = JsonConvert.Load<Table[]>(tablesFile);
            Reservations = JsonConvert.Load<Reservation[]>(reservationsFile);
        }

        public void GetReservations(string hours, string minutes)
        {
            Console.WriteLine($"Reservasjoner for kl.{hours}:{minutes}:\n");

            foreach (var reservation in Reservations)
            {
                if (IsWithinTimeFrame(hours, minutes, reservation))
                {
                    Console.WriteLine($"Bord: {reservation.Table}\n" +
                        $"Navn: {reservation.CustomerName}\nTlf: {reservation.CustomerPhone}\nAntall: {reservation.Count}\n" +
                        $"Reservert fra {reservation.StartTime} til {reservation.StartTime + 200}\n");
                }
            }
        }

        public void BookReservation(string hours, string minutes, int customerCount, string customerName, string customerPhone)
        {
            var table = FindTable(hours, minutes, customerCount);
            if (table != null)
            {
                new Reservation()
                {
                    Count = Convert.ToInt32(customerCount),
                    CustomerName = customerName,
                    CustomerPhone = customerPhone,
                    StartTime = Convert.ToInt32(hours + minutes),
                    Table = table.Name
                };
                Console.WriteLine($"{customerName} booket bord {table.Name} for {customerCount} personer");
                return;
            }
            Console.WriteLine("Ingen bord tilgjengelig");
        }

        private Table FindTable(string hours, string minutes, int customerCount)
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

        public bool TableIsFree(string hours, string minutes, string tableName)
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
            int endTime = reservation.StartTime + 200;

            if (reservation.StartTime <= searchedTime && endTime >= searchedTime) return true;
            else return false;
        }
    }
}