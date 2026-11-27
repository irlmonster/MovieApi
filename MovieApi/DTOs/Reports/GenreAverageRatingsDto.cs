namespace MovieApi.DTOs.Reports
{
    public record GenreAverageRatingsDto
    {
        public string Genre { get; init; } = null!;
        public double AverageRating { get; init; }
        public int ReviewCount { get; init; }
    }
}
