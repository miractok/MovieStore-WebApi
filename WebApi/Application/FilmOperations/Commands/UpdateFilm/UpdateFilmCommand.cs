using WebApi.DBOperations;

namespace WebApi.Application.FilmOperations.Commands.UpdateFilm
{
    public class UpdateFilmCommand
    {
        private readonly IMovieStoreDbContext _context;
        public int FilmId { get; set; }
        public UpdateFilmModel Model { get; set; }

        public UpdateFilmCommand(IMovieStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var film = _context.Films.SingleOrDefault(x=> x.Id == FilmId);
            if(film == null)
                throw new InvalidOperationException("Film could not be found!");

            if(_context.Films.Any(x=> x.Title.ToLower() == Model.Title.ToLower() && x.Id != FilmId))
                throw new InvalidOperationException("Film already exists!");

            film.Title = Model.Title != default ? Model.Title : film.Title;
            film.PublishDate = Model.PublishDate != default ? Model.PublishDate : film.PublishDate;
            film.GenreId = Model.GenreId != default ? Model.GenreId : film.GenreId;
            film.Price = Model.Price != default ? Model.Price : film.Price;

            _context.Films.Update(film);
            _context.SaveChanges();
        }

    }

    public class UpdateFilmModel
    {
        public string Title { get; set; }
        public DateTime PublishDate { get; set; }
        public int GenreId { get; set; }
        public double Price { get; set; }
    }
}