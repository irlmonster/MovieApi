using System.ComponentModel.DataAnnotations;

namespace MovieApi.DTOs
{
    public record MovieActorCreateDto
    {
        [Required]
        public int ActorId { get; init; }

        [Required]
        [MaxLength(100)]
        public string Role { get; init; } = string.Empty;
    }
}