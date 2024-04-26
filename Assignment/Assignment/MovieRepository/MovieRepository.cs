using System.Collections.Generic;
using System.Linq;
using Assignment.Models;
using Assignment.MovieRepository;
using DocumentFormat.OpenXml.InkML;
using Microsoft.EntityFrameworkCore;

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
        public IEnumerable<MovieReview> GetAll(int? movieId = null, int pageNumber = 1, int pageSize = 10)
        {
            IQueryable<MovieReview> query = _db.MovieReviews;

            // Filter by MovieId if provided
            if (movieId.HasValue && movieId > 0)
            {
                query = query.Where(m => m.MovieId == movieId);
            }

            // Pagination
            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            return query.ToList();
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
        public Booking GetBooking(int id)
        {
            return _db.Bookings.FirstOrDefault(x=>x.BookingId==id);
        }
     
            



            
        
         

         
    }
}