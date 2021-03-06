﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BetterCalendar.Models
{
    public class EventViewModel
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
