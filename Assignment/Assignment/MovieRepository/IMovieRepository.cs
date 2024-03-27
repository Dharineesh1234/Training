using Assignment.Models;

namespace Assignment.MovieRepository
{
    public interface IMovieRepository
    {
        void Create(MovieReview movieReview);
        List<MovieReview> GetAll();
        MovieReview GetById(int id);
        void Update(MovieReview movieReview);
    }
}
