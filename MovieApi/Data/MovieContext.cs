using Microsoft.EntityFrameworkCore;
using MovieApi.Models;

public class MovieContext(DbContextOptions<MovieContext> options) : DbContext(options)
{
    public DbSet<Movie> Movies { get; set; } = default!;
    public DbSet<MovieDetails> MovieDetails { get; set; }

}
