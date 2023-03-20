using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.DBOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MovieStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<MovieStoreDbContext>>()))
            {
                if(context.Films.Any())
                {
                    return;
                }

                context.Films.AddRange(
                    new Film
                    {
                        Title = "Lord Of The Rings: The Fellowship of the ring",
                        PublishDate = new DateTime(2001), 
                        GenreId = 1,
                        Price = 12
                    },
                    new Film
                    {
                        Title = "X-Men",
                        PublishDate = new DateTime(2000), 
                        GenreId = 2,
                        Price = 8
                    },
                    new Film
                    {
                        Title = "King Kong",
                        PublishDate = new DateTime(2005), 
                        GenreId = 3,
                        Price = 5
                    },
                    new Film
                    {
                        Title = "Green Book",
                        PublishDate = new DateTime(2005), 
                        GenreId = 3,
                        Price = 10
                    }
                );

                context.Directors.AddRange(
                    new Director{NameSurname = "Peter Jackson"},
                    new Director{NameSurname = "Bryan Singer"},
                    new Director{NameSurname = "Peter Farrelly"}
                );

                context.Actors.AddRange(
                    new Actor{NameSurname = "Viggo Mortensen"},
                    new Actor{NameSurname = "Ian McKellen"},
                    new Actor{NameSurname = "Jack Black"}
                );

                context.Genres.AddRange(
                    new Genre{Name = "Fantasy"},
                    new Genre{Name = "Science Fiction"},
                    new Genre{Name = "Adventure"}
                );

                context.DirectorFilms.AddRange(
                    new DirectorFilm{FilmId = 1, DirectorId = 1},
                    new DirectorFilm{FilmId = 2, DirectorId = 2},
                    new DirectorFilm{FilmId = 3, DirectorId = 1},
                    new DirectorFilm{FilmId = 4, DirectorId = 3}
                );

                context.ActorFilms.AddRange(
                    new ActorFilm{FilmId = 1, ActorId = 1},
                    new ActorFilm{FilmId = 1, ActorId = 2},
                    new ActorFilm{FilmId = 2, ActorId = 2},
                    new ActorFilm{FilmId = 3, ActorId = 3},
                    new ActorFilm{FilmId = 4, ActorId = 1}
                );

                context.SaveChanges();
            }
        }
    }
}