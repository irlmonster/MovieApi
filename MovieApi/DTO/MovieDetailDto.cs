namespace MovieApi.DTOs
{
    public record MovieDetailDto
    {
        // Filmdata
        public int Id { get; init; }
        public string Title { get; init; } = string.Empty;
        public int Year { get; init; }
        public int Duration { get; init; }
        public string Director { get; init; } = string.Empty;
        public List<string> Genres { get; init; } = new();

        // Från MovieDetails
        public string Synopsis { get; init; } = string.Empty;
        public string Language { get; init; } = string.Empty;
        public decimal Budget { get; init; }

        // Relationer
        public List<ReviewDto> Reviews { get; init; } = new();
        public List<MovieActorDto> Actors { get; init; } = new();
    }
}