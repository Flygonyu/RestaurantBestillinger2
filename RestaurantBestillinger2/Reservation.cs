using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBestillinger2
{
    internal class Reservation
    {
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public int Count { get; set; }
        public int StartTime { get; set; }
        public string Table { get; set; }
    }
}
