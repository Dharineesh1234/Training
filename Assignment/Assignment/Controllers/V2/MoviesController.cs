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
    [ApiVersion("2.0")]


    public class MoviesController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;

        public MoviesController(IMovieRepository movieRepository)
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
        public IActionResult GetAll()
        {
            var movieReviews = _movieRepository.GetAll();
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
    }
}