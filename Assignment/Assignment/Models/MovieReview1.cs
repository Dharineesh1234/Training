using System;
using System.Collections.Generic;

namespace Assignment.Models
{
    public partial class MovieReview1
    {
        public int MoviereviewId { get; set; }
        public int MovieId { get; set; }
        public string ReviewerName { get; set; } = null!;
        public string Comments { get; set; } = null!;
        public int Rating { get; set; }
    }
}
