using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.DBOperations
{
    public interface IMovieStoreDbContext
    {
        public DbSet<Film> Films { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<ActorFilm> ActorFilms { get; set; }
        public DbSet<DirectorFilm> DirectorFilms { get; set; }

        int SaveChanges();
    }
}