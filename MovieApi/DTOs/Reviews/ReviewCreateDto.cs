using System.ComponentModel.DataAnnotations;

namespace MovieApi.DTOs.Reviews
{
    public record ReviewCreateDto
    {
        [Required]
        [MaxLength(100)]
        public string ReviewerName { get; init; } = string.Empty;

        [Required]
        [MaxLength(1000)]
        public string Comment { get; init; } = string.Empty;

        [Range(1, 5)]
        public int Rating { get; init; }
    }
}