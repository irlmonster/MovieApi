using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApi.Models;

namespace MovieApi.Data.Configurations
{
    public class DirectorConfiguration : IEntityTypeConfiguration<Director>
    {
        public void Configure(EntityTypeBuilder<Director> builder)
        {
            builder.Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(100);

            // "Christopher Nolan" får bara finnas en gång
            builder.HasIndex(d => d.Name)
                .IsUnique();
        }
    }
}
