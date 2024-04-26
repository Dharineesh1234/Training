namespace Movies.Models
{
    public class MoviesReviews
    {
        public int ReviewId { get; set; }
        public int? MovieId { get; set; }
        public string? ReviewerName { get; set; }
        public string? ReviewText { get; set; }
        public int? Rating { get; set; }
    }
}
