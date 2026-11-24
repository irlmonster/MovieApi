using System.ComponentModel.DataAnnotations;

namespace MovieApi.Models
{
    public class Movie
    {

        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        public int Duration { get; set; }

        public int Year { get; set; }

        [Required]
        public string Genre { get; set; } = string.Empty;


        // Navigation property för MovieDetails
        public MovieDetails? MovieDetails { get; set; }


        // Navigation property för Reviews
        public ICollection<Review> Reviews { get; set; } = new List<Review>();













    }
}
