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

            builder.Property(m => m.Genre)
                .IsRequired()
                .HasMaxLength(50);


            // 1:1 - En film har en MovieDetails, som hör till en film. 
            // MovieDetails har ett MovieId som är en foreign key till Movie.
            builder.HasOne(m => m.MovieDetails)
                .WithOne(d => d.Movie)
                .HasForeignKey<MovieDetails>(d => d.MovieId);

            // 1:n - En film kan ha flera reviews, men en review hör till en film.
            builder.HasMany(m => m.Reviews)
                .WithOne(r => r.Movie)
                .HasForeignKey(r => r.MovieId);

            // 1:n - En film kan ha flera actors och en actor kan höra till flera filmer.
            builder.HasMany(m => m.Actors)
                .WithMany(a => a.Movies)
                .UsingEntity<Dictionary<string, object>>(
                "MovieActor",
                j => j.HasOne<Actor>().WithMany().HasForeignKey("ActorId"),
                j => j.HasOne<Movie>().WithMany().HasForeignKey("MovieId"),
                j => j.HasKey("MovieId", "ActorId"));



        }
    }
}
