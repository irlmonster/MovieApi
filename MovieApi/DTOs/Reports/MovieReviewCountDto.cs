namespace MovieApi.DTOs.Reports
{
    public record MovieReviewCountDto
    {
        public int Id { get; init; }
        public string Title { get; init; } = null!;
        public int ReviewCount { get; init; }
    }
}
