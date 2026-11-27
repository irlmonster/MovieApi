namespace MovieApi.DTOs.Movies
{
    public record MovieActorDto
    {
        public int ActorId { get; init; }
        public string Name { get; init; } = string.Empty;
        public int BirthYear { get; init; }
        public string Role { get; init; } = string.Empty;
    }
}