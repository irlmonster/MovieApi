namespace MovieApi.Models
{
    public class MovieDetails
    {
        public int Id { get; set; }
        public string Synopsis { get; set; } = string.Empty;
        public string Language { get; set; } = string.Empty;
        public int budget { get; set; }


        // FK till Movie
        public int MovieId { get; set; }
        // Navigation property
        public Movie Movie { get; set; } = null!;
    }
}
