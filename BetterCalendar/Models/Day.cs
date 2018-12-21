using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BetterCalendar.Models
{
    public class Day
    {
        public DateTime FullDate { get; set; }
        public string Name { get; set; }
        public int DayNumber { get; set; }
        public bool HasEvents { get; set; }
    }
}
