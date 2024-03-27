using System.Collections.Generic;
using System.Linq;
using Assignment.Models;
using Assignment.MovieRepository;

namespace Assignment.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MoviesContext _db;

        public MovieRepository(MoviesContext db)
        {
            _db = db;
        }

        public void Create(MovieReview movieReview)
        {
            _db.Add(movieReview);
            _db.SaveChanges();
        }

        public List<MovieReview> GetAll()
        {
            return _db.MovieReviews.ToList();
        }

        public MovieReview GetById(int id)
        {
            return _db.MovieReviews.FirstOrDefault(x => x.ReviewId == id);
        }

        public void Update(MovieReview movieReview)
        {
            _db.Update(movieReview);
            _db.SaveChanges();
        }
    }
}