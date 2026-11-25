using Microsoft.EntityFrameworkCore;
using MovieApi.Models;


namespace MovieApi.Data
{
    public class MovieContext(DbContextOptions<MovieContext> options) : DbContext(options)
    {
        public DbSet<Movie> Movies { get; set; } = default!;
        public DbSet<MovieDetails> MovieDetails { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Actor> Actors { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MovieContext).Assembly);
        }

    }
}