﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Reserveringssysteem
{
    public class Reservation
    {
        public int ID { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public TimeSpan Duration { get; set; }

        [Required]
        public Team Team { get; set; }

        [Required]
        public Boat Boat { get; set; }

        public Reservation(Boat b, DateTime d)
        {
            DateTime = d;
            Boat = b;
        }
        public Reservation() { }
    }
}
