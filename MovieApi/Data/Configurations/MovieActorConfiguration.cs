using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApi.Models;

namespace MovieApi.Data.Configurations
{
    public class MovieActorConfiguration : IEntityTypeConfiguration<MovieActor>
    {
        public void Configure(EntityTypeBuilder<MovieActor> builder)
        {
            builder.ToTable("MovieActor");

            // Sammansatt nyckel: kombinationen MovieId + ActorId är unik
            builder.HasKey(ma => new { ma.MovieId, ma.ActorId });

            builder.Property(ma => ma.Role)
                .IsRequired()
                .HasMaxLength(100);

            // 1:M - En film har många MovieActors
            builder.HasOne(ma => ma.Movie)
                .WithMany(m => m.MovieActors)
                .HasForeignKey(ma => ma.MovieId);

            // 1:M - En skådis har många MovieActors
            builder.HasOne(ma => ma.Actor)
                .WithMany(a => a.MovieActors)
                .HasForeignKey(ma => ma.ActorId);
        }
    }
}