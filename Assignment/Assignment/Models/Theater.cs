using System;
using System.Collections.Generic;

namespace Assignment.Models
{
    public partial class Theater
    {
        public Theater()
        {
            ShowTimes = new HashSet<ShowTime>();
        }

        public int TheaterId { get; set; }
        public string? TheaterName { get; set; }
        public string? Location { get; set; }

        public virtual ICollection<ShowTime> ShowTimes { get; set; }
    }
}
