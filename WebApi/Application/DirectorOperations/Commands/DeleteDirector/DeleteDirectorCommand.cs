using WebApi.DBOperations;

namespace WebApi.Application.DirectorOperations.Commands.DeleteDirector
{
    public class DeleteDirectorCommand
    {
        private readonly IMovieStoreDbContext _context;
        public int DirectorId;

        public DeleteDirectorCommand(IMovieStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var director = _context.Directors.SingleOrDefault(x => x.Id == DirectorId);
            if(director == null)
                throw new InvalidOperationException("Director could not be found!");

            var directorCheck = _context.DirectorFilms.Where(x=> x.DirectorId == director.Id).Any();
            if(directorCheck)
                throw new InvalidOperationException("Director cannot be deleted! First remove Director from the Film.");

            _context.Directors.Remove(director);
            _context.SaveChanges();
        }
    }
}