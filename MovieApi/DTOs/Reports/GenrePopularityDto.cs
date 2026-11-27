using System.ComponentModel.DataAnnotations;

namespace MovieApi.DTOs.Reports
{
    public record GenrePopularityDto
    {
        public string Genre { get; init; } = null!;
        public int MovieCount { get; init; }
    }
}
