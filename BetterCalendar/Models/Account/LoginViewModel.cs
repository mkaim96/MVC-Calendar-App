using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BetterCalendar.Models.Account
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name="Nazwa użytkownika")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="Hasło")]
        public string Password { get; set; }

        [Display(Name = "Zapamiętać Cię?")]
        public bool RememberMe { get; set; }

    }
}
