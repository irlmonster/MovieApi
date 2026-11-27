using System.ComponentModel.DataAnnotations;

namespace MovieApi.DTOs.Movies
{
    public record MovieActorCreateDto
    {
        [Range(1, int.MaxValue)]
        public int ActorId { get; init; }

        [Required]
        [MaxLength(100)]
        public string Role { get; init; } = string.Empty;
    }
}