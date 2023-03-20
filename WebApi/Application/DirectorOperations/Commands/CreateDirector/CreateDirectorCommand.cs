using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.DirectorOperations.Commands.CreateDirector
{
    public class CreateDirectorCommand
    {
        public CreateDirectorViewModel Model { get; set; }
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateDirectorCommand(IMapper mapper, IMovieStoreDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public void Handle()
        {
            var director = _context.Directors.SingleOrDefault(x => x.NameSurname == Model.NameSurname);
            if(director != null)
                throw new InvalidOperationException("This Director already exists.");

            director = _mapper.Map<Director>(Model);

            _context.Directors.Add(director);
            _context.SaveChanges();
        }
    }

    public class CreateDirectorViewModel
    {
        public string NameSurname { get; set; }
    }
}