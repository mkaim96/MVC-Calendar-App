using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BetterCalendar.Data
{
    public class ApplicationUser : IdentityUser
    {
        public IEnumerable<Event> Events { get; set; }
    }
}