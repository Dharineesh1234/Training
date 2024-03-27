using System;
using System.Collections.Generic;

namespace Assignment.Models
{
    public partial class MovieReview
    {
        public int ReviewId { get; set; }
        public int? MovieId { get; set; }
        public string? ReviewerName { get; set; }
        public string? ReviewText { get; set; }
        public int? Rating { get; set; }
    }
}
