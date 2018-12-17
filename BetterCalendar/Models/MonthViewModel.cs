using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BetterCalendar.Models
{
    public class MonthViewModel
    {
        public DateTime Date { get; set; }

        public string MonthName { get; set; }

        public string PrevMonthName { get; set; }

        public string NextMonthName { get; set; }

        // Nedded to render calendar card in html
        public int DaysToSkipInFirstWeek { get; set; }

        public List<Day> Days { get; set; }
    }
}
