using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Application.DirectorOperations.Queries.GetDirectorDetails
{
    public class GetDirectorDetailsQuery
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public int DirectorId { get; set; }

        public GetDirectorDetailsQuery(IMapper mapper, IMovieStoreDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public DirectorViewIdModel Handle()
        {
            var director = _context.Directors.Include(x => x.DirectorFilm).ThenInclude(x => x.Film).Where(x => x.Id == DirectorId).SingleOrDefault();
            
            if(director == null)
                throw new InvalidOperationException("The Id you entered does not match any director.");

            DirectorViewIdModel vm = _mapper.Map<DirectorViewIdModel>(director);

            return vm;

        }
    }

    public class DirectorViewIdModel
    {
        public string NameSurname { get; set; }
        public virtual ICollection<string> Films { get; set; }
        public bool IsActive { get; set; }
    }
}