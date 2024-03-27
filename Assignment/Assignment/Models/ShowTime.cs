using System;
using System.Collections.Generic;

namespace Assignment.Models
{
    public partial class ShowTime
    {
        public ShowTime()
        {
            Bookings = new HashSet<Booking>();
        }

        public int ShowTimeId { get; set; }
        public int? MovieId { get; set; }
        public int? TheaterId { get; set; }
        public DateTime? StartTime { get; set; }

        public virtual Movie? Movie { get; set; }
        public virtual Theater? Theater { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
