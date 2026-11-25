using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Data;
using MovieApi.DTO;
using MovieApi.DTOs;
using MovieApi.Models;

[Route("api/[controller]")]
[ApiController]
public class MoviesController : ControllerBase
{
    private readonly MovieContext _context;
    public MoviesController(MovieContext context)
    {
        _context = context;
    }

    // GET: api/Movies
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Movie>>> GetMovie()
    {
        var movies = await _context.Movies
            .Select(m => new MovieDto
            {
                Id = m.Id,
                Title = m.Title,
                Year = m.Year,
                Genres = m.Genres.Select(g => g.Name).ToList()
            })
        .ToListAsync();

        return Ok(movies);
    }

    // GET: api/Movies/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Movie>> GetMovie(int id)
    {
        var movie = await _context.Movies
            .Where(m => m.Id == id)
            .Select(m => new MovieDto
            {
                Id = m.Id,
                Title = m.Title,
                Year = m.Year,
                Duration = m.Duration,
                Genres = m.Genres.Select(g => g.Name).ToList()
            })
            .FirstOrDefaultAsync();

        if (movie == null)
        {
            return NotFound();
        }

        return Ok(movie);
    }

    // PUT: api/Movies/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutMovie(int id, MovieUpdateDto dto)
    {
        // 1. Hämta filmen MED dess genrer och detaljer
        var movie = await _context.Movies
            .Include(m => m.Genres)
            .Include(m => m.MovieDetails)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (movie == null)
            return NotFound();

        // 2. Kolla genrerna, precis som i POST
        var genres = await _context.Genres
            .Where(g => dto.GenreIds.Contains(g.Id))
            .ToListAsync();

        if (genres.Count != dto.GenreIds.Distinct().Count())
            return BadRequest("En eller flera genrer finns inte.");

        // 3. Skriv över värdena
        movie.Title = dto.Title;
        movie.Year = dto.Year;
        movie.Duration = dto.Duration;

        movie.Genres.Clear();
        foreach (var genre in genres)
            movie.Genres.Add(genre);

        if (movie.MovieDetails == null)
            movie.MovieDetails = new MovieDetails();

        movie.MovieDetails.Synopsis = dto.Synopsis;
        movie.MovieDetails.Language = dto.Language;
        movie.MovieDetails.Budget = dto.Budget;

        // 4. Spara
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // POST: api/Movies
    [HttpPost]
    public async Task<ActionResult<MovieDto>> PostMovie(MovieCreateDto dto)
    {
        // 1. Hämta genrerna användaren valt
        var genres = await _context.Genres
            .Where(g => dto.GenreIds.Contains(g.Id))
            .ToListAsync();

        // 2. Fanns alla? Annars har användaren skickat ett Id som inte finns
        if (genres.Count != dto.GenreIds.Distinct().Count())
            return BadRequest("En eller flera genrer finns inte.");

        // 3. Packa upp kartongen till en riktig film
        var movie = new Movie
        {
            Title = dto.Title,
            Year = dto.Year,
            Duration = dto.Duration,
            Genres = genres,
            MovieDetails = new MovieDetails
            {
                Synopsis = dto.Synopsis,
                Language = dto.Language,
                Budget = dto.Budget
            }
        };

        // 4. Spara
        _context.Movies.Add(movie);
        await _context.SaveChangesAsync();

        // 5. Packa en ny kartong att skicka tillbaka
        var result = new MovieDto
        {
            Id = movie.Id,
            Title = movie.Title,
            Year = movie.Year,
            Duration = movie.Duration,
            Genres = genres.Select(g => g.Name).ToList()
        };

        return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, result);
    }

    // DELETE: api/Movies/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMovie(int id)
    {
        var movie = await _context.Movies.FindAsync(id);

        if (movie == null)
            return NotFound();

        _context.Movies.Remove(movie);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // GET: api/Movies/5/details
    [HttpGet("{id}/details")]
    public async Task<ActionResult<MovieDetailDto>> GetMovieDetails(int id)
    {
        var movie = await _context.Movies
            .Where(m => m.Id == id)
            .Select(m => new MovieDetailDto
            {
                // Filmdata
                Id = m.Id,
                Title = m.Title,
                Year = m.Year,
                Duration = m.Duration,
                Genres = m.Genres.Select(g => g.Name).ToList(),

                // Från MovieDetails
                Synopsis = m.MovieDetails != null ? m.MovieDetails.Synopsis : string.Empty,
                Language = m.MovieDetails != null ? m.MovieDetails.Language : string.Empty,
                Budget = m.MovieDetails != null ? m.MovieDetails.Budget : 0,

                // Relationer
                Reviews = m.Reviews.Select(r => new ReviewDto
                {
                    Id = r.Id,
                    ReviewerName = r.ReviewerName,
                    Comment = r.Comment,
                    Rating = r.Rating
                }).ToList(),

                Actors = m.Actors.Select(a => new ActorDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    BirthYear = a.BirthYear
                }).ToList()
            })
            .FirstOrDefaultAsync();

        if (movie == null)
            return NotFound();

        return Ok(movie);
    }




    private bool MovieExists(int? id)
    {
        return _context.Movies.Any(e => e.Id == id);
    }
}
