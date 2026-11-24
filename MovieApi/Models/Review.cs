namespace MovieApi.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string ReviewerName { get; set; } = string.Empty;
        public string Comment{ get; set; } = string.Empty;
        public int Rating { get; set; }

        // FK till Movie
        public int MovieId { get; set; }
        // Navigation property
        public Movie Movie { get; set; } = null!;
    }
}
