namespace MovieApi.DTOs.Reports
{
    public record MovieRatingDto
    {
        public int Id { get; init; }
        public string Title { get; init; } = null!;
        public double AverageRating { get; init; }
    }
}
