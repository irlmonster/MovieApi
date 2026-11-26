using System.ComponentModel.DataAnnotations;

namespace MovieApi.DTOs
{
    public record ActorCreateDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; init; } = string.Empty;

        [Range(1850, 2026)]
        public int BirthYear { get; init; }
    }
}
