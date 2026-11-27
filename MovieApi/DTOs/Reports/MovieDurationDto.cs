namespace MovieApi.DTOs.Reports
{
    public record MovieDurationDto
    {
        public int Id { get; init; }
        public string Title { get; init; } = null!;
        public int Duration { get; init; }
        public string Country { get; init; } = null!;
    }
}
