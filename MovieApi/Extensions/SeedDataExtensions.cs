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
            var curtis = new Director { Name = "Richard Curtis" };
            var ritchie = new Director { Name = "Guy Ritchie" };
            var boyle = new Director { Name = "Danny Boyle" };
            var tykwer = new Director { Name = "Tom Tykwer" };
            var becker = new Director { Name = "Wolfgang Becker" };
            var petersen = new Director { Name = "Wolfgang Petersen" };
            var holm = new Director { Name = "Hannes Holm" };
            var oplev = new Director { Name = "Niels Arden Oplev" };
            var stahelski = new Director { Name = "Chad Stahelski" };

            // Skådisar
            var keanu = new Actor { Name = "Keanu Reeves", BirthYear = 1964 };
            var carrieAnne = new Actor { Name = "Carrie-Anne Moss", BirthYear = 1967 };
            var tomHanks = new Actor { Name = "Tom Hanks", BirthYear = 1956 };
            var robinWright = new Actor { Name = "Robin Wright", BirthYear = 1966 };
            var jimCarrey = new Actor { Name = "Jim Carrey", BirthYear = 1962 };
            var hughGrant = new Actor { Name = "Hugh Grant", BirthYear = 1960 };
            var emmaThompson = new Actor { Name = "Emma Thompson", BirthYear = 1959 };
            var statham = new Actor { Name = "Jason Statham", BirthYear = 1967 };
            var bradPitt = new Actor { Name = "Brad Pitt", BirthYear = 1963 };
            var cillian = new Actor { Name = "Cillian Murphy", BirthYear = 1976 };
            var franka = new Actor { Name = "Franka Potente", BirthYear = 1974 };
            var bruhl = new Actor { Name = "Daniel Brühl", BirthYear = 1978 };
            var prochnow = new Actor { Name = "Jürgen Prochnow", BirthYear = 1941 };
            var lassgard = new Actor { Name = "Rolf Lassgård", BirthYear = 1955 };
            var nyqvist = new Actor { Name = "Michael Nyqvist", BirthYear = 1960 };
            var noomi = new Actor { Name = "Noomi Rapace", BirthYear = 1979 };

            // Länder
            var usa = new Country { Name = "USA" };
            var uk = new Country { Name = "UK" };
            var germany = new Country { Name = "Germany" };
            var sweden = new Country { Name = "Sweden" };

            var movies = new List<Movie>
            {
                // ===== USA =====
                new Movie
                {
                    Title = "The Matrix",
                    Year = 1999,
                    Duration = 136,
                    Genres = { action, sciFi },
                    Director = wachowski,
                    Country = usa,
                    MovieActors =
                    {
                        new MovieActor { Actor = keanu, Role = "Neo" },
                        new MovieActor { Actor = carrieAnne, Role = "Trinity" }
                    },
                    MovieDetails = new MovieDetails { Synopsis = "A hacker discovers reality is a simulation.", Language = "English", Budget = 63000000m },
                    Reviews =
                    {
                        new Review { ReviewerName = "Anna", Comment = "Mind-blowing!", Rating = 5 },
                        new Review { ReviewerName = "Erik", Comment = "Lite förvirrande men bra.", Rating = 4 }
                    }
                },
                new Movie
                {
                    Title = "Forrest Gump",
                    Year = 1994,
                    Duration = 142,
                    Genres = { drama, comedy },
                    Director = zemeckis,
                    Country = usa,
                    MovieActors =
                    {
                        new MovieActor { Actor = tomHanks, Role = "Forrest Gump" },
                        new MovieActor { Actor = robinWright, Role = "Jenny Curran" }
                    },
                    MovieDetails = new MovieDetails { Synopsis = "A kind man witnesses decades of American history.", Language = "English", Budget = 55000000m },
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
                    Country = usa,
                    MovieActors =
                    {
                        new MovieActor { Actor = jimCarrey, Role = "Truman Burbank" }
                    },
                    MovieDetails = new MovieDetails { Synopsis = "A man learns his whole life is a TV show.", Language = "English", Budget = 60000000m },
                    Reviews =
                    {
                        new Review { ReviewerName = "Johan", Comment = "Smart och rolig.", Rating = 4 },
                        new Review { ReviewerName = "Sara", Comment = "Okej.", Rating = 3 }
                    }
                },
                new Movie
                {
                    Title = "Cast Away",
                    Year = 2000,
                    Duration = 143,
                    Genres = { drama },
                    Director = zemeckis,
                    Country = usa,
                    MovieActors = { new MovieActor { Actor = tomHanks, Role = "Chuck Noland" } },
                    MovieDetails = new MovieDetails { Synopsis = "A FedEx employee is stranded on a deserted island.", Language = "English", Budget = 90000000m },
                    Reviews =
                    {
                        new Review { ReviewerName = "Anna", Comment = "Wilsoooon!", Rating = 5 },
                        new Review { ReviewerName = "Erik", Comment = "Lite långsam i mitten.", Rating = 3 }
                    }
                },
                new Movie
                {
                    Title = "John Wick",
                    Year = 2014,
                    Duration = 101,
                    Genres = { action },
                    Director = stahelski,
                    Country = usa,
                    MovieActors = { new MovieActor { Actor = keanu, Role = "John Wick" } },
                    MovieDetails = new MovieDetails { Synopsis = "A retired hitman seeks revenge for his dog.", Language = "English", Budget = 20000000m },
                    Reviews =
                    {
                        new Review { ReviewerName = "Johan", Comment = "Bästa actionfilmen på länge.", Rating = 5 },
                        new Review { ReviewerName = "Sara", Comment = "Mycket skjutande.", Rating = 3 },
                        new Review { ReviewerName = "Lisa", Comment = "Rättvisa för hunden!", Rating = 5 }
                    }
                },

                // ===== UK =====
                new Movie
                {
                    Title = "Love Actually",
                    Year = 2003,
                    Duration = 135,
                    Genres = { comedy, drama },
                    Director = curtis,
                    Country = uk,
                    MovieActors =
                    {
                        new MovieActor { Actor = hughGrant, Role = "David" },
                        new MovieActor { Actor = emmaThompson, Role = "Karen" }
                    },
                    MovieDetails = new MovieDetails { Synopsis = "Interconnected love stories in London before Christmas.", Language = "English", Budget = 40000000m },
                    Reviews = { new Review { ReviewerName = "Maja", Comment = "Måste ses varje jul.", Rating = 4 } }
                },
                new Movie
                {
                    Title = "Snatch",
                    Year = 2000,
                    Duration = 104,
                    Genres = { comedy, action },
                    Director = ritchie,
                    Country = uk,
                    MovieActors =
                    {
                        new MovieActor { Actor = statham, Role = "Turkish" },
                        new MovieActor { Actor = bradPitt, Role = "Mickey O'Neil" }
                    },
                    MovieDetails = new MovieDetails { Synopsis = "Criminals chase a stolen diamond through London.", Language = "English", Budget = 10000000m },
                    Reviews =
                    {
                        new Review { ReviewerName = "Erik", Comment = "Förstod inte vad Brad Pitt sa.", Rating = 4 },
                        new Review { ReviewerName = "Johan", Comment = "Kaotiskt och kul.", Rating = 5 }
                    }
                },
                new Movie
                {
                    Title = "28 Days Later",
                    Year = 2002,
                    Duration = 113,
                    Genres = { sciFi, drama },
                    Director = boyle,
                    Country = uk,
                    MovieActors = { new MovieActor { Actor = cillian, Role = "Jim" } },
                    MovieDetails = new MovieDetails { Synopsis = "A man wakes from a coma to find London deserted.", Language = "English", Budget = 8000000m },
                    Reviews = { new Review { ReviewerName = "Sara", Comment = "Läskig!", Rating = 4 } }
                },

                // ===== Germany =====
                new Movie
                {
                    Title = "Run Lola Run",
                    Year = 1998,
                    Duration = 81,
                    Genres = { action },
                    Director = tykwer,
                    Country = germany,
                    MovieActors = { new MovieActor { Actor = franka, Role = "Lola" } },
                    MovieDetails = new MovieDetails { Synopsis = "Lola has twenty minutes to find 100,000 marks.", Language = "German", Budget = 1750000m },
                    Reviews = { new Review { ReviewerName = "Anna", Comment = "Snabb och snygg.", Rating = 4 } }
                },
                new Movie
                {
                    Title = "Good Bye Lenin!",
                    Year = 2003,
                    Duration = 121,
                    Genres = { comedy, drama },
                    Director = becker,
                    Country = germany,
                    MovieActors = { new MovieActor { Actor = bruhl, Role = "Alex Kerner" } },
                    MovieDetails = new MovieDetails { Synopsis = "A son hides the fall of the Berlin Wall from his mother.", Language = "German", Budget = 4800000m },
                    Reviews =
                    {
                        new Review { ReviewerName = "Lisa", Comment = "Rolig och sorglig på samma gång.", Rating = 5 },
                        new Review { ReviewerName = "Maja", Comment = "Fin.", Rating = 4 }
                    }
                },
                new Movie
                {
                    Title = "Das Boot",
                    Year = 1981,
                    Duration = 149,
                    Genres = { drama, action },
                    Director = petersen,
                    Country = germany,
                    MovieActors = { new MovieActor { Actor = prochnow, Role = "Der Kaleun" } },
                    MovieDetails = new MovieDetails { Synopsis = "The crew of a German U-boat during World War II.", Language = "German", Budget = 15000000m },
                    Reviews = { new Review { ReviewerName = "Erik", Comment = "Klaustrofobiskt mästerverk.", Rating = 5 } }
                },

                // ===== Sweden =====
                new Movie
                {
                    Title = "En man som heter Ove",
                    Year = 2015,
                    Duration = 116,
                    Genres = { comedy, drama },
                    Director = holm,
                    Country = sweden,
                    MovieActors = { new MovieActor { Actor = lassgard, Role = "Ove" } },
                    MovieDetails = new MovieDetails { Synopsis = "A grumpy widower's life changes when new neighbours move in.", Language = "Swedish", Budget = 5000000m },
                    Reviews =
                    {
                        new Review { ReviewerName = "Johan", Comment = "Grät som ett barn.", Rating = 5 },
                        new Review { ReviewerName = "Sara", Comment = "Mysig.", Rating = 4 }
                    }
                },
                new Movie
                {
                    Title = "Män som hatar kvinnor",
                    Year = 2009,
                    Duration = 152,
                    Genres = { drama },
                    Director = oplev,
                    Country = sweden,
                    MovieActors =
                    {
                        new MovieActor { Actor = nyqvist, Role = "Mikael Blomkvist" },
                        new MovieActor { Actor = noomi, Role = "Lisbeth Salander" }
                    },
                    MovieDetails = new MovieDetails { Synopsis = "A journalist and a hacker investigate a decades-old disappearance.", Language = "Swedish", Budget = 13000000m },
                    Reviews =
                    {
                        new Review { ReviewerName = "Anna", Comment = "Noomi är grym.", Rating = 5 },
                        new Review { ReviewerName = "Lisa", Comment = "För brutal för mig.", Rating = 2 }
                    }
                }
            };

            context.Countries.AddRange(usa, uk, germany, sweden);
            context.Movies.AddRange(movies);
            context.SaveChanges();
        }
    }
}