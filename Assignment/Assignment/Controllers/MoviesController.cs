using Assignment.Models;
using Assignment.MovieRepository;
using Assignment.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Movie.Controllers
{



    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]


    [Route("api/[controller]")]
    [ApiVersion("1.0")]


    public class MovieController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;

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

        public IActionResult GetAll()
        {
            var movieReviews = _movieRepository.GetAll();
            return Ok(movieReviews);
        }

        [HttpGet]
        [Route("GetById/{id}")]
        [Authorize]
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
    }
}