using BetterCalendar.Data;
using BetterCalendar.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace BetterCalendar.services
{
    public class CalendarService
    {
        private GregorianCalendar Calendar { get; set; }


        public CalendarService()
        {
            Calendar = new GregorianCalendar();
        }

        #region MonthViewModel

        public string GetMonthName(DateTime date)
        {
            return MonthToString(date.Month);
        }

        public string GetPrevMonthName(DateTime date)
        {
            // set date to one month earlier
            date = date.AddMonths(-1);

            //and return name of it
            return MonthToString(date.Month);
        }

        public string GetNextMonthName(DateTime date)
        {
            // set date to one month later
            date = date.AddMonths(1);

            // and return name of it
            return MonthToString(date.Month);

        }

        // nedded to render calendar card in html
        public int GetNumberOfDaysToSkipInMonth(DateTime date)
        {
            // set date to first day of month to check which day of week it is
            date = new DateTime(date.Year, date.Month, 1);

            // if day of week is for example 6 then we have to skip
            // 5 days (generate 5 blank squares in html)
            // NOTE: sunday is 0 so change it to 7
            int daysToSkip = (Calendar.GetDayOfWeek(date) == 0 ? 7 : (int)Calendar.GetDayOfWeek(date)) - 1;

            return daysToSkip;

        }

        public List<Day> GetListOfDays(DateTime date, List<Event> events)
        {
            var days = new List<Day>();
            
            for (int i = 1; i <= Calendar.GetDaysInMonth(date.Year, date.Month); i++)
            {
                DateTime tmpDate = new DateTime(date.Year, date.Month, i);


                days.Add(new Day
                {
                    FullDate = tmpDate,
                    DayNumber = i,
                    HasEvents = events.Where(a => a.Date == tmpDate).Any()
                });
            }
            return days;
        }
        #endregion

        #region DayViewModel

        public string GetMonthNameDop(DateTime date)
        {
            return MonthToStringDop(date.Month);
        }

        public string GetDayName(DateTime date)
        {
            return DayToString(date.DayOfWeek); 
        }

        #endregion

        #region Helpers

        private string MonthToString(int monthNumber)
        {
            string name = "";
            switch (monthNumber)
            {
                case 1:
                    name = "Styczeń";
                    break;
                case 2:
                    name = "Luty";
                    break;
                case 3:
                    name = "Marzec";
                    break;
                case 4:
                    name = "Kwiecień";
                    break;
                case 5:
                    name = "Maj";
                    break;
                case 6:
                    name = "Czerwiec";
                    break;
                case 7:
                    name = "Lipiec";
                    break;
                case 8:
                    name = "Sierpień";
                    break;
                case 9:
                    name = "Wrzesień";
                    break;
                case 10:
                    name = "Pażdziernik";
                    break;
                case 11:
                    name = "Listopad";
                    break;
                case 12:
                    name = "Grudzień";
                    break;
            }
            return name;
        }

        private string MonthToStringDop(int monthNumber)
        {
            string name = "";
            switch (monthNumber)
            {
                case 1:
                    name = "Stycznia";
                    break;
                case 2:
                    name = "Lutego";
                    break;
                case 3:
                    name = "Marca";
                    break;
                case 4:
                    name = "Kwietnia";
                    break;
                case 5:
                    name = "Maja";
                    break;
                case 6:
                    name = "Czerwca";
                    break;
                case 7:
                    name = "Lipca";
                    break;
                case 8:
                    name = "Sierpnia";
                    break;
                case 9:
                    name = "Września";
                    break;
                case 10:
                    name = "Października";
                    break;
                case 11:
                    name = "Listopada";
                    break;
                case 12:
                    name = "Grudnia";
                    break;
            }
            return name;
        }

        private string DayToString(DayOfWeek day)
        {
            string name = "";

            switch (day)
            {
                case DayOfWeek.Monday:
                    name = "Poniedziałek";
                    break;
                case DayOfWeek.Tuesday:
                    name = "Wtorek";
                    break;
                case DayOfWeek.Wednesday:
                    name = "Środa";
                    break;
                case DayOfWeek.Thursday:
                    name = "Czwartek";
                    break;
                case DayOfWeek.Friday:
                    name = "Piątek";
                    break;
                case DayOfWeek.Saturday:
                    name = "Sobota";
                    break;
                case DayOfWeek.Sunday:
                    name = "Niedziela";
                    break;
            }

            return name;
        }



        #endregion



    }
}
