using WebApi.DBOperations;

namespace WebApi.Application.DirectorOperations.Commands.UpdateDirector
{
    public class UpdateDirectorCommand
    {
        private readonly IMovieStoreDbContext _context;
        public int DirectorId { get; set; }
        public UpdateDirectorViewModel Model { get; set; }
        public UpdateDirectorCommand(IMovieStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var director = _context.Directors.SingleOrDefault(x => x.Id == DirectorId);
            if(director == null)
                throw new InvalidOperationException("Director could bot be found!");
            if(_context.Directors.Any(x => x.NameSurname.ToLower() == Model.NameSurname.ToLower() && x.Id != DirectorId))
                throw new InvalidOperationException("Director already exists!");

            director.NameSurname = Model.NameSurname != default ? Model.NameSurname : director.NameSurname;

            _context.Directors.Update(director);
            _context.SaveChanges();
        }
    }

    public class UpdateDirectorViewModel
    {
        public string NameSurname { get; set; }
    }
}