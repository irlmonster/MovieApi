namespace MovieApi.DTOs.Reports
{
    public record GenreTopMoviesDto
    {
        public string Genre { get; init; } = null!;
        public List<MovieRatingDto> TopMovies { get; init; } = new();
    }
}
