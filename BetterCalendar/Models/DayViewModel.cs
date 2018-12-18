using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BetterCalendar.Models
{
    public class DayViewModel
    {
        public DateTime FullDate { get; set; }
        public string DayName { get; set; }
        public int DayNumber { get; set; }
        public string MonthNameDop { get; set; }

        public CreateEventViewModel newEvent { get; set; }

        public List<EventViewModel> Events { get; set; }
    }
}
