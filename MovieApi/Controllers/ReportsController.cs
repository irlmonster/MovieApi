using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Data;
using MovieApi.DTOs.Reports;

namespace MovieApi.Controllers
{
    [Route("api/reports")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly MovieContext _context;

        public ReportsController(MovieContext context)
        {
            _context = context;
        }


        // Rapporter
        // GET: api/reports/genres/popular 
        [HttpGet("genres/popular")]
        public async Task<ActionResult<IEnumerable<GenrePopularityDto>>> GetPopularGenres()
        {
            var result = await _context.Genres
                .Select(g => new GenrePopularityDto
                {
                    Genre = g.Name,
                    MovieCount = g.Movies.Count()
                })
                .OrderByDescending(g => g.MovieCount)
                .ToListAsync();
            return Ok(result);
        }

        // GET: /api/reports/movies/with-most-reviews
        [HttpGet("movies/with-most-reviews")]
        public async Task<ActionResult<MovieReviewCountDto>> GetMoviesWithMostReviews()
        {
            var result = await _context.Movies
                .Select(m => new MovieReviewCountDto
                {
                    Id = m.Id,
                    Title = m.Title,
                    ReviewCount = m.Reviews.Count()
                })
                .OrderByDescending(m => m.ReviewCount)
                .FirstOrDefaultAsync();

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // GET: /api/reports/movies/longest-per-country
        [HttpGet("movies/longest-per-country")]
        public async Task<ActionResult<IEnumerable<MovieDurationDto>>> GetLongestMoviesPerCountry()
        {
            var result = await _context.Countries
                .Where(c => c.Movies.Any())
                .Select(c => c.Movies
                    .OrderByDescending(m => m.Duration)
                    .Select(m => new MovieDurationDto
                    {
                        Id = m.Id,
                        Title = m.Title,
                        Duration = m.Duration,
                        Country = c.Name
                    })
                    .First())
                .OrderByDescending(x => x.Duration)   // sortera vinnarna, längst först
                .ToListAsync();

            return Ok(result);
        }


        // GET: /api/reports/actors/most-active
        [HttpGet("actors/most-active")]
        public async Task<ActionResult<IEnumerable<ActorMovieCountDto>>> GetMostActiveActors()
        {
            var result = await _context.Actors
                .Select(a => new ActorMovieCountDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    MovieCount = a.MovieActors.Count()
                })
                .OrderByDescending(a => a.MovieCount)
                .Take(5)
                .ToListAsync();
            return Ok(result);
        }


        // GET: /api/reports/movies/average-ratings
        [HttpGet("movies/average-ratings")]
        public async Task<ActionResult<IEnumerable<GenreAverageRatingsDto>>> GetAverageRatingGenre()
        {
            var result = await _context.Genres
                .Select(g => new GenreAverageRatingsDto
                {
                    Genre = g.Name,

                    ReviewCount = g.Movies
                    .SelectMany(m => m.Reviews)
                        .Count(),

                    AverageRating = g.Movies
                    .SelectMany(m => m.Reviews)
                        .Average(r => (double?)r.Rating) ?? 0
                })
                .OrderByDescending(r => r.AverageRating)
                .ToListAsync();
            return Ok(result);
        }



        // För varje genre:
        //   ta genrens filmer
        //   räkna ut varje films snittbetyg
        //   sortera filmerna på snittbetyg, högst först
        //   ta de 5 första
        // Packa: genrens namn + listan med top 5

        // GET: /api/reports/movies/top5pergenre
        [HttpGet("movies/top5pergenre")]
        public async Task<ActionResult<IEnumerable<GenreTopMoviesDto>>> GetTop5PerGenre()
        {
            var result = await _context.Genres
                .Select(g => new GenreTopMoviesDto
                {
                    Genre = g.Name,
                    TopMovies = g.Movies
                        .Select(m => new MovieRatingDto
                        {
                            Id = m.Id,
                            Title = m.Title,
                            AverageRating = m.Reviews.Average(r => (double?)r.Rating) ?? 0
                        })
                        .OrderByDescending(x => x.AverageRating)
                        .Take(5)
                        .ToList()
                })
                .ToListAsync();

            return Ok(result);
        }
    }
}
