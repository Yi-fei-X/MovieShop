using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]     // Attribute based routing
    [ApiController]
    public class MoviesController : ControllerBase
    {
        // create an api method that shows top 30 revenue/grossing movies
        // so that my SPA, iOS and Android app show those movies in the home screen
        private readonly IMovieService _movieService;   //DI
        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }
        // http://localhost/api/movies/3
        [HttpGet]   // Required when using Swagger
        [Route("{id:int}")]
        public async Task<IActionResult> GetMovie(int id)   //Model binding
        {
            var movie = await _movieService.GetMovieDetails(id);
            if (movie == null)
            {
                return NotFound($"No Movie Found for {id}");
            }
            return Ok(movie);
        }

        [HttpGet]
        [Route("genre/{genreId:int}")]
        // http://localhost:5001/api/movies/genre/5?pagesize=30&pageIndex=35
        // [?pagesize=30&pageIndex=35] Key value pair with query string. After ? is query string, pagesize is key, 30 is value.
        // many movies belonging to a genre=> 
        // pagination 
        // 2000 movies for 5
        // 30 movies per page =>
        // show how many page number
        // 2000/30 => 67 pages
        public async Task<IActionResult> GetMoviesByGenres(int genreId, [FromQuery] int pagesize = 30, [FromQuery] int pageIndex = 1)
        {
            // server - side pagination (Only get those necessary data from database. Good for performance)
            // 1 to 30 rows
            // click on page 2 => 31 to 60
            // 3 => 61 to 90
            // LINQ moviegenres.skip(pageindex-1).take(pagesize).tolistasync()
            // SQL offset 0 and fetch next 30
            // First vs FirstOrDefault vs Single Vs SingleOrDefault
            return Ok();
        }

        // create the api method that shows top 30 movies , json data
        [HttpGet]
        [Route("toprevenue")]    // Attribute based routing
        // http://localhost/api/movies/toprevenue   // Not recommend to put Get in the URL. Get should be a part of method, not URL.
        // API
        public async Task<IActionResult> GetTopRevenueMovies()
        {
            var movies = await _movieService.GetTop30RevenueMovies();

            // return JSON data and Http Status Code //

            if (!movies.Any())
            {
                // return 404(Http Status Code)
                return NotFound("No Movies Found");
            }

            // return 200 OK (Http Status Code) + movies (Data)
            return Ok(movies);  // Automatically convert to JSON data using System.Text.Json

            // for coverting C# objects to Json objects there are 2 ways
            // before .NET Core 3, we used NewtonSoft.Json library
            // Mirosoft created their own JSON Serialization library
            // System.Text.Json

        }

    }
}
