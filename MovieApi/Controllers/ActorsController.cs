using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Data;
using MovieApi.DTOs;
using MovieApi.Models;

namespace MovieApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private readonly MovieContext _context;

        public ActorsController(MovieContext context)
        {
            _context = context;
        }

        // GET: api/actors
        [HttpGet("actors")]
        public async Task<ActionResult<IEnumerable<ActorDto>>> GetActors()
        {
            var actors = await _context.Actors
                .Select(a => new ActorDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    BirthYear = a.BirthYear
                })
                .ToListAsync();

            return Ok(actors);
        }

        // GET: api/actors/5
        [HttpGet("actors/{id}")]
        public async Task<ActionResult<ActorDto>> GetActor(int id)
        {
            var actor = await _context.Actors
                .Where(a => a.Id == id)
                .Select(a => new ActorDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    BirthYear = a.BirthYear
                })
                .FirstOrDefaultAsync();

            if (actor == null)
                return NotFound();

            return Ok(actor);
        }

        // POST: api/actors
        [HttpPost("actors")]
        public async Task<ActionResult<ActorDto>> PostActor(ActorCreateDto dto)
        {
            var actor = new Actor
            {
                Name = dto.Name,
                BirthYear = dto.BirthYear
            };

            _context.Actors.Add(actor);
            await _context.SaveChangesAsync();

            var result = new ActorDto
            {
                Id = actor.Id,
                Name = actor.Name,
                BirthYear = actor.BirthYear
            };

            return CreatedAtAction(nameof(GetActor), new { id = actor.Id }, result);
        }

        // PUT: api/actors/5
        [HttpPut("actors/{id}")]
        public async Task<IActionResult> PutActor(int id, ActorUpdateDto dto)
        {
            var actor = await _context.Actors.FindAsync(id);
            if (actor == null)
                return NotFound();

            actor.Name = dto.Name;
            actor.BirthYear = dto.BirthYear;

            await _context.SaveChangesAsync();

            return NoContent();
        }


        // POST: api/movies/5/actors
        // Body: { "actorId": 4, "role": "Main antagonist" }
        [HttpPost("movies/{movieId}/actors")]
        public async Task<IActionResult> AddActorToMovie(int movieId, MovieActorCreateDto dto)
        {
            if (!await _context.Movies.AnyAsync(m => m.Id == movieId))
                return NotFound("Filmen finns inte.");

            if (!await _context.Actors.AnyAsync(a => a.Id == dto.ActorId))
                return NotFound("Skådespelaren finns inte.");

            var alreadyLinked = await _context.MovieActors
                .AnyAsync(ma => ma.MovieId == movieId && ma.ActorId == dto.ActorId);

            if (alreadyLinked)
                return Conflict("Skådespelaren är redan med i filmen.");

            var movieActor = new MovieActor
            {
                MovieId = movieId,          // från URL:en
                ActorId = dto.ActorId,      // från bodyn
                Role = dto.Role             // från bodyn
            };

            _context.MovieActors.Add(movieActor);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}