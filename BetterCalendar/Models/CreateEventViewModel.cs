using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BetterCalendar.Models
{
    public class CreateEventViewModel
    {
        [Required]
        [Display(Name = "Tytuł")]
        public string Title { get; set; }

        [Display(Name = "Opis")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Rozpoczęcie")]
        [DataType(DataType.Time)]
        public DateTime Start { get; set; }

        [Display(Name = "Zakończenie")]
        [DataType(DataType.Time)]
        public DateTime? End { get; set; }
    }
}
