using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.DirectorOperations.Queries.GetDirector
{
    public class GetDirectorsQuery
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetDirectorsQuery(IMapper mapper, IMovieStoreDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public List<DirectorViewModel> Handle()
        {
            var directorList = _context.Directors.Include(x => x.DirectorFilm).ThenInclude(x => x.Film).OrderBy(x => x.Id).ToList<Director>();
            List<DirectorViewModel> vm = _mapper.Map<List<DirectorViewModel>>(directorList);

            return vm;
        }
    }

    public class DirectorViewModel
    {
        public string NameSurname { get; set; }
        public ICollection<string> Films { get; set; }
        public bool IsActive { get; set; }
    }
}