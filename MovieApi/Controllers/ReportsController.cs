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
        public async Task<ActionResult<IEnumerable<MovieReviewCountDto>>> GetMoviesWithMostReviews()
        {
            var result = await _context.Movies
                .Select(m => new MovieReviewCountDto
                {
                    Id = m.Id,
                    Title = m.Title,
                    ReviewCount = m.Reviews.Count()
                })
                .OrderByDescending(m => m.ReviewCount)
                .ToListAsync();

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

    }
}
