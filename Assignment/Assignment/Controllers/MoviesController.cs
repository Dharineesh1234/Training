using Assignment.Models;
using Assignment.MovieRepository;
using Assignment.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Movie.Controllers
{



    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]


    [Route("api/[controller]")]
    [ApiVersion("1.0")]


    public class MovieController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;
        private readonly MoviesContext _context;

        public MovieController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        [HttpPost]
        [Route("Create")]
        public IActionResult Create([FromBody] MovieReview movieReview)
        {
            _movieRepository.Create(movieReview);
            return Ok(movieReview);
        }

      
        [HttpGet]
        [Route("GetAll")]
        [Authorize]

        public IActionResult GetAll(int? MovieId, int pageNumber = 1, int pageSize = 10)
        
        {
            var movieReviews = _movieRepository.GetAll(MovieId, pageNumber, pageSize);
            return Ok(movieReviews);
        }
        [HttpGet]
        [Route("GetById/{id}")]
        
        public IActionResult GetById(int id)
        {
            var movieReview = _movieRepository.GetById(id);
            if (movieReview == null)
            {
                return NotFound();
            }
            return Ok(movieReview);
        }

        [HttpPut]
        [Route("update")]
        public IActionResult Update(MovieReview movieReview)
        {
            _movieRepository.Update(movieReview);
            return Ok(movieReview);
        }
        [HttpGet]
        [Route("BOOKING")]
        public IActionResult GetBooking(int id)
        {
            var booking= _movieRepository.GetBooking(id);
            if(booking == null)
            {
                return NotFound();
            }
            return Ok(booking);
        }
       
    }
}