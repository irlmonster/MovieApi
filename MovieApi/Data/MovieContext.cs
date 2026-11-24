using Microsoft.EntityFrameworkCore;

public class MovieContext(DbContextOptions<MovieContext> options) : DbContext(options)
{
    public DbSet<MovieApi.Models.Movie> Movies { get; set; } = default!;
}
