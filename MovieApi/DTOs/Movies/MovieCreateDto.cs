using System.ComponentModel.DataAnnotations;

namespace MovieApi.DTOs.Movies
{
    public record MovieCreateDto
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; init; } = string.Empty;

        [Range(1888, 2100)]
        public int Year { get; init; }

        [Range(1, 800)]
        public int Duration { get; init; }

        public List<int> GenreIds { get; init; } = new();

        [Range(1, int.MaxValue)]
        public int DirectorId { get; init; }
        
        [Range(1, int.MaxValue)]
        public int CountryId { get; init; }

        // MovieDetails skapas samtidigt som filmen
        [Required]
        public string Synopsis { get; init; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Language { get; init; } = string.Empty;

        [Range(0, double.MaxValue)]
        public decimal Budget { get; init; }
    }
}
