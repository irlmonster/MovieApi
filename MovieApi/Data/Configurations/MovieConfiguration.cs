using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApi.Models;

namespace MovieApi.Data.Configurations
{
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.Property(m => m.Title)
                .IsRequired()
                .HasMaxLength(100);


            // 1:1 - En film har en MovieDetails, som hör till en film. 
            // MovieDetails har ett MovieId som är en foreign key till Movie.
            builder.HasOne(m => m.MovieDetails)
                .WithOne(d => d.Movie)
                .HasForeignKey<MovieDetails>(d => d.MovieId);

            // 1:n - En film kan ha flera reviews, men en review hör till en film.
            builder.HasMany(m => m.Reviews)
                .WithOne(r => r.Movie)
                .HasForeignKey(r => r.MovieId);

            // N:M - En film kan ha flera genres och en genre kan höra till flera filmer.
            builder.HasMany(m => m.Genres)
                .WithMany(g => g.Movies)
                .UsingEntity(
                    "MovieGenre",
                    l => l.HasOne(typeof(Genre)).WithMany().HasForeignKey("GenreId"),
                    r => r.HasOne(typeof(Movie)).WithMany().HasForeignKey("MovieId"),
                    j => j.HasKey("MovieId", "GenreId"));

            // M:1 - En film har en regissör, en regissör kan ha många filmer.
            builder.HasOne(m => m.Director)
                .WithMany(d => d.Movies)
                .HasForeignKey(m => m.DirectorId)
                .OnDelete(DeleteBehavior.Restrict);

            // M:1 - En film har ett land, ett land kan ha många filmer.
            builder.HasOne(m => m.Country)
                .WithMany(c => c.Movies)
                .HasForeignKey(m => m.CountryId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
