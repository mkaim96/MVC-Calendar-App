using BetterCalendar.Data;
using BetterCalendar.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BetterCalendar.Controllers
{
    [Authorize]
    public class ApiController : Controller
    {
        private EventsService _eventsService;
        private UserManager<ApplicationUser> _userManager;
        private CalendarService _calendarService;

        public ApiController(
            EventsService eventsService,
            UserManager<ApplicationUser> userManager,
            CalendarService calendarService
            )
        {
            _eventsService = eventsService;
            _userManager = userManager;
            _calendarService = calendarService;
        }


        [Route("api/events/get-all")]
        public IActionResult GetAllEvents()
        {
            var events = _eventsService.GetAll(CurrentUserId());

            return Ok(events);
        }

        [Route("api/events/get-by-month/{date}")]
        public IActionResult GetEventsByMonth(DateTime date)
        {
            var events = _eventsService.GetByMonth(date, CurrentUserId());
            return Ok(events);
        }

        public IActionResult GetMonthInfo()
        {
            
            return Ok();
        }

        #region Helpers

        private string CurrentUserId()
        {
            return _userManager.GetUserId(HttpContext.User);
        }

        #endregion
    }
}
