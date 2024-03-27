using System;
using System.Collections.Generic;

namespace Assignment.Models
{
    public partial class Movie
    {
        public Movie()
        {
            ShowTimes = new HashSet<ShowTime>();
        }

        public int MovieId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Genre { get; set; }

        public virtual ICollection<ShowTime> ShowTimes { get; set; }
    }
}
