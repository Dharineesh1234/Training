using Assignment.Models;

namespace Assignment.MovieRepository
{
    public interface IMovieRepository
    {
        void Create(MovieReview movieReview);
        IEnumerable<MovieReview> GetAll(int? movieId = null, int pageNumber = 1, int pageSize = 10);
        MovieReview GetById(int id);
        void Update(MovieReview movieReview);
        public Booking GetBooking(int id);

     
    }
}
