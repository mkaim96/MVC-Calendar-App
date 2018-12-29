using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BetterCalendar.Models
{
    public class TimelineViewModel
    {
        public List<DayTimeline> Days { get; set; }
    }

    public class DayTimeline
    {
        public DateTime Date { get; set; }
        public string MonthNameShort { get; set; }
        public List<string> EventTitles { get; set; }

        public string Odmiana()
        {
            int count = EventTitles.Count;

            if (count == 0) return "Wydarzeń";
            if (count == 1) return "Wydarzenie";

            // znajdz liczbe jednosci

            int jednosci;

            if (count >= 10)
            {
                var str = count.ToString();
                jednosci = Convert.ToInt32(str.Substring(str.Length - 1, 1));

                if (jednosci >= 4) return "Wydarzenia";
                else return "Wydarzeń";
            }

            if (count >= 4) return "Wydarzenia";
            else return "Wydarzeń";


        }
    }
}
