using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BetterCalendar.Data;
using BetterCalendar.Models;
using BetterCalendar.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BetterCalendar.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private CalendarService calendarService;
        private ApplicationDbContext context;
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;

        public HomeController(
            CalendarService calendarService,
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager
            )
        {
            this.calendarService = calendarService;
            this.context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public IActionResult Index()
        {
            var date = DateTime.Now;
            var userId = GetCurrentUserId();


            // needed to mark day squares with events
            var events = context.Events
                .Where(a => a.Date.Month == DateTime.Now.Month && a.User.Id == userId)
                .Select(a => new Event { Date = a.Date })
                .ToList();

            var model = new MonthViewModel
            {
                Date = date,
                MonthName = calendarService.GetMonthName(date),
                PrevMonthName = calendarService.GetPrevMonthName(date),
                NextMonthName = calendarService.GetNextMonthName(date),
                DaysToSkipInFirstWeek = calendarService.GetNumberOfDaysToSkipInMonth(date),
                Days = calendarService.GetListOfDays(date, events)
            };

            return View(model);
        }
        
        [Route("previous-month")]
        public IActionResult PrevMonth(DateTime date)
        {
            var userId = GetCurrentUserId();
            date = date.AddMonths(-1);

            var events = context.Events
                .Where(a => a.Date.Month == date.Month && a.User.Id == userId)
                .ToList();

            var model = new MonthViewModel
            {
                Date = date,
                MonthName = calendarService.GetMonthName(date),
                PrevMonthName = calendarService.GetPrevMonthName(date),
                NextMonthName = calendarService.GetNextMonthName(date),
                DaysToSkipInFirstWeek = calendarService.GetNumberOfDaysToSkipInMonth(date),
                Days = calendarService.GetListOfDays(date, events)
            };

            return View("Index", model);
        }

        [Route("next-month")]
        public IActionResult NextMonth(DateTime date)
        {
            var userId = GetCurrentUserId();
            date = date.AddMonths(1);

            var events = context.Events
                .Where(a => a.Date.Month == date.Month && a.User.Id == userId)
                .ToList();


            var model = new MonthViewModel
            {
                Date = date,
                MonthName = calendarService.GetMonthName(date),
                PrevMonthName = calendarService.GetPrevMonthName(date),
                NextMonthName = calendarService.GetNextMonthName(date),
                DaysToSkipInFirstWeek = calendarService.GetNumberOfDaysToSkipInMonth(date),
                Days = calendarService.GetListOfDays(date, events)
            };

            return View("Index", model);
        }

        [Route("day/{date}")]
        public IActionResult Day(DateTime date)
        {
            // find users events for given date
            var userId = GetCurrentUserId();
            var events = context.Events.Where(a => a.Date == date && a.User.Id == userId);

            // map events to EventViewModel 
            var eventsViewModel = events.Select(a => new EventViewModel
            {
                Id = a.Id,
                Description = a.Description,
                End = a.End,
                Start = a.Start,
                Title = a.Title
            }).ToList();

            var model = new DayViewModel
            {
                FullDate = date,
                DayName = calendarService.GetDayName(date),
                MonthNameDop = calendarService.GetMonthNameDop(date),
                DayNumber = date.Day,
                Events = eventsViewModel
                 
            };

            return View(model);
        }

        [Route("day/{date}/new-event")]
        public IActionResult CreateEvent()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("day/{date}/new-event")]
        public async Task<IActionResult> CreateEvent(CreateEventViewModel model, DateTime date)
        {
            if (ModelState.IsValid)
            {
                var EndDate = Convert.ToDateTime(model.End);

                // Constructs date of the end of the event
                // (user input is only hours and minutes, rest od DateTime is deafult, so we take it from route)
                //
                // if model.End is null then set end of the event to null 
                // otherwise take day, month and year from route
                // hours and minutes from user input, and seconds is always 0
                DateTime? eventEnd = model.End == null ? null : new DateTime?(
                        new DateTime(date.Year, date.Month, date.Day, EndDate.Hour, EndDate.Minute, 0)
                    );

                var userId = GetCurrentUserId();
                var newEvent = new Event
                {
                    Date = date,
                    Title = model.Title,
                    Description = model.Description,
                    Start = new DateTime(date.Year, date.Month, date.Day, model.Start.Hour, model.Start.Minute, 0),
                    End = eventEnd,
                    User = await userManager.FindByIdAsync(userId)
                };

                context.Events.Add(newEvent);
                context.SaveChanges();

                return RedirectToAction("Day");
            }
            return View(model);
        }

        [Route("delete")]
        public IActionResult Delete(int eventId, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            Event e = context.Events.Find(eventId);

            if (e == null) return NotFound();

            context.Events.Remove(e);
            context.SaveChanges();

            return LocalRedirect(returnUrl);
        }


        [Route("all-events")]
        public IActionResult AllEvents()
        {
            var userId = GetCurrentUserId();

            // find users events
            var usersEvents = context.Events
                .Where(a => a.User.Id == userId)
                .OrderBy(a => a.Start);

            List<EventViewModel> model = usersEvents.Select(a => new EventViewModel
            {
                Id = a.Id,
                Title = a.Title,
                Description = a.Description,
                End = a.End,
                Start = a.Start
            }).ToList();


            return View(model);
        }
        #region Helpers

        private string GetCurrentUserId()
        {
            return userManager.GetUserId(HttpContext.User);
        }
        #endregion
    }
}