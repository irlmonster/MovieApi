using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Data;
using MovieApi.DTO;
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

        // POST: api/movies/5/actors/3
        [HttpPost("movies/{movieId}/actors/{actorId}")]
        public async Task<IActionResult> AddActorToMovie(int movieId, int actorId)
        {
            // 1. Hämta filmen MED dess skådisar
            var movie = await _context.Movies
                .Include(m => m.Actors)
                .FirstOrDefaultAsync(m => m.Id == movieId);

            if (movie == null)
                return NotFound("Filmen finns inte.");

            // 2. Hämta skådisen
            var actor = await _context.Actors.FindAsync(actorId);
            if (actor == null)
                return NotFound("Skådespelaren finns inte.");

            // 3. Är de redan kopplade?
            if (movie.Actors.Any(a => a.Id == actorId))
                return Conflict("Skådespelaren är redan med i filmen.");

            // 4. Koppla ihop
            movie.Actors.Add(actor);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}