using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BetterCalendar.Models.Api
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Nazwa użytkownika")]
        public string UsernameOrEmail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }
    }
}
