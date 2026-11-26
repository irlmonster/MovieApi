using MovieApi.Data;
using MovieApi.Models;

namespace MovieApi.Extensions
{
    public static class SeedDataExtensions
    {
        public static void SeedData(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<MovieContext>();

            // Finns det redan filmer? Då har vi redan seedat data och returnerar istället.
            if (context.Movies.Any()) return;

            // Genrer
            var action = new Genre { Name = "Action" };
            var sciFi = new Genre { Name = "Sci-Fi" };
            var drama = new Genre { Name = "Drama" };
            var comedy = new Genre { Name = "Comedy" };

            // Regissörer
            var wachowski = new Director { Name = "Lana Wachowski" };
            var zemeckis = new Director { Name = "Robert Zemeckis" };
            var weir = new Director { Name = "Peter Weir" };

            // Skådisar
            var keanu = new Actor { Name = "Keanu Reeves", BirthYear = 1964 };
            var carrieAnne = new Actor { Name = "Carrie-Anne Moss", BirthYear = 1967 };
            var tomHanks = new Actor { Name = "Tom Hanks", BirthYear = 1956 };
            var robinWright = new Actor { Name = "Robin Wright", BirthYear = 1966 };
            var jimCarrey = new Actor { Name = "Jim Carrey", BirthYear = 1962 };

            var movies = new List<Movie>
            {
                new Movie
                {
                    Title = "The Matrix",
                    Year = 1999,
                    Duration = 136,
                    Genres = { action, sciFi },
                    Director = wachowski,
                    MovieActors =
                    {
                        new MovieActor { Actor = keanu, Role = "Neo" },
                        new MovieActor { Actor = carrieAnne, Role = "Trinity" }
                    },
                    MovieDetails = new MovieDetails
                    {
                        Synopsis = "A hacker discovers reality is a simulation.",
                        Language = "English",
                        Budget = 63000000m
                    },
                    Reviews =
                    {
                        new Review { ReviewerName = "Anna", Comment = "Mind-blowing!", Rating = 5 },
                        new Review { ReviewerName = "Erik", Comment = "Lite förvirrande men bra.", Rating = 4 }
                    },

                },
                new Movie
                {
                    Title = "Forrest Gump",
                    Year = 1994,
                    Duration = 142,
                    Genres = { drama, comedy },
                    Director = zemeckis,
                    MovieActors =
                    {
                        new MovieActor { Actor = tomHanks, Role = "Forrest Gump" },
                        new MovieActor { Actor = robinWright, Role = "Jenny Curran" }
                    },
                    MovieDetails = new MovieDetails
                    {
                        Synopsis = "A kind man witnesses decades of American history.",
                        Language = "English",
                        Budget = 55000000m
                    },
                    Reviews =
                    {
                        new Review { ReviewerName = "Lisa", Comment = "Grät hela filmen.", Rating = 5 }
                    }
                },
                new Movie
                {
                    Title = "The Truman Show",
                    Year = 1998,
                    Duration = 103,
                    Genres = { drama, comedy },
                    Director = weir,
                    MovieActors =
                    {
                        new MovieActor { Actor = jimCarrey, Role = "Truman Burbank" }
                    },
                    MovieDetails = new MovieDetails
                    {
                        Synopsis = "A man learns his whole life is a TV show.",
                        Language = "English",
                        Budget = 60000000m
                    },
                    Reviews =
                    {
                        new Review { ReviewerName = "Johan", Comment = "Smart och rolig.", Rating = 4 },
                        new Review { ReviewerName = "Sara", Comment = "Okej.", Rating = 3 }
                    }
                }
            };

            context.Movies.AddRange(movies);
            context.SaveChanges();
        }
    }
}