namespace MovieApi.DTOs.Reports
{
    public record ActorMovieCountDto
    {
        public int Id { get; init; }
        public string Name { get; init; } = null!;
        public int MovieCount { get; init; }
    }
}
