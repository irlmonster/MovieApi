using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Data;
using MovieApi.DTOs.Reviews;
using MovieApi.Models;

namespace MovieApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly MovieContext _context;

        public ReviewsController(MovieContext context)
        {
            _context = context;
        }

        // GET: api/movies/5/reviews
        [HttpGet("movies/{movieId}/reviews")]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReviews(int movieId)
        {
            var movieExists = await _context.Movies.AnyAsync(m => m.Id == movieId); // AnyAsync istället för FindAsync, behöver inte hämta hela objektet, bara kolla om det finns
            if (!movieExists)
                return NotFound();

            var reviews = await _context.Reviews
                .Where(r => r.MovieId == movieId)
                .Select(r => new ReviewDto
                {
                    Id = r.Id,
                    ReviewerName = r.ReviewerName,
                    Comment = r.Comment,
                    Rating = r.Rating
                })
                .ToListAsync();

            return Ok(reviews);
        }

        // POST: api/movies/5/reviews
        [HttpPost("movies/{movieId}/reviews")]
        public async Task<ActionResult<ReviewDto>> PostReview(int movieId, ReviewCreateDto dto)
        {
            var movieExists = await _context.Movies.AnyAsync(m => m.Id == movieId);
            if (!movieExists)
                return NotFound();

            var review = new Review
            {
                ReviewerName = dto.ReviewerName,
                Comment = dto.Comment,
                Rating = dto.Rating,
                MovieId = movieId
            };

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            var result = new ReviewDto
            {
                Id = review.Id,
                ReviewerName = review.ReviewerName,
                Comment = review.Comment,
                Rating = review.Rating
            };

            return CreatedAtAction(nameof(GetReviews), new { movieId }, result);
        }

        // DELETE: api/reviews/7
        [HttpDelete("reviews/{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
                return NotFound();

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}