namespace MovieApi.Models
{
    public class Movie
    {

        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public int Duration { get; set; }

        public int Year { get; set; }


        // Navigation property för MovieDetails
        public MovieDetails? MovieDetails { get; set; }

        // Navigation property för Reviews
        public ICollection<Review> Reviews { get; set; } = new List<Review>();

        // Navigation property för Actors
        public ICollection<Actor> Actors { get; set; } = new List<Actor>();

        // Navigation property för Genre
        public ICollection<Genre> Genres { get; set; } = new List<Genre>();


    }
}
