using WebApi.DBOperations;
using WebApi.Entities;

namespace TestSetup
{
    public static class DataGenerator
    {
        public static void AddData(this MovieStoreDbContext context)
        {
            context.Films.AddRange(
                    new Film{ Title = "Lord Of The Rings: The Fellowship of the ring", PublishDate = new DateTime(2001),  GenreId = 1, Price = 12 },
                    new Film{ Title = "X-Men", PublishDate = new DateTime(2000), GenreId = 2, Price = 8 },
                    new Film{ Title = "King Kong", PublishDate = new DateTime(2005), GenreId = 3, Price = 5 },
                    new Film{ Title = "Green Book", PublishDate = new DateTime(2005), GenreId = 3, Price = 10 });

            context.Directors.AddRange(
                    new Director{NameSurname = "Peter Jackson"},
                    new Director{NameSurname = "Bryan Singer"},
                    new Director{NameSurname = "Peter Farrelly"},
                    new Director{NameSurname = "TestDirectorDummy"}
                );

            context.Actors.AddRange(
                    new Actor{NameSurname = "Viggo Mortensen"},
                    new Actor{NameSurname = "Ian McKellen"},
                    new Actor{NameSurname = "Jack Black"},
                    new Actor{NameSurname = "TestActorDummy"}
                );

            context.Genres.AddRange(
                    new Genre{Name = "Fantasy"},
                    new Genre{Name = "Science Fiction"},
                    new Genre{Name = "Adventure"},
                    new Genre{Name = "GenreTestsDummy"}
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

            context.Customers.AddRange(
                    new Customer{NameSurname = "mirac", Email = "mirac@gmail.com", Password = "123456"}
                );

            context.FavouriteGenres.AddRange(
                    new FavouriteGenre{CustomerId = 1, GenreId = 1},
                    new FavouriteGenre{CustomerId = 1, GenreId = 2},
                    new FavouriteGenre{CustomerId = 1, GenreId = 3}
                );
                
            context.Orders.AddRange(
                    new Order{CustomerId = 1, FilmId = 1, PurchaseDate = new DateTime(2023,01,12)},
                    new Order{CustomerId = 1, FilmId = 2, PurchaseDate = new DateTime(2023,01,12)}
                );

            context.SaveChanges();
        }
    }
}