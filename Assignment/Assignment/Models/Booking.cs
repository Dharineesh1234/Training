using System;
using System.Collections.Generic;

namespace Assignment.Models
{
    public partial class Booking
    {
        public int BookingId { get; set; }
        public int? ShowTimeId { get; set; }
        public string? UserId { get; set; }
        public int? NumberOfStickers { get; set; }

        public virtual ShowTime? ShowTime { get; set; }
    }
}
