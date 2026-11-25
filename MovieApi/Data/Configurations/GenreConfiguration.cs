using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApi.Models;

namespace MovieApi.Data.Configurations
{
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.Property(g => g.Name)
                .IsRequired()
                .HasMaxLength(50);

            // "Drama" får bara finnas en gång
            builder.HasIndex(g => g.Name)
                .IsUnique();
        }
    }
}
