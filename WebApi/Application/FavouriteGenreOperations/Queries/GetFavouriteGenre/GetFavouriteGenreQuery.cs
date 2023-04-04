using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.FavouriteGenreOperations.Queries.GetFavouriteGenre
{
    public class GetFavouriteGenreQuery
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetFavouriteGenreQuery(IMapper mapper, IMovieStoreDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public List<FavouriteGenreViewModel> Handle()
        {
            var favouriteGenreList = _context.FavouriteGenres.Include(x => x.Customer).Include(x => x.Genre).OrderBy(x=> x.Id).ToList<FavouriteGenre>();
            List<FavouriteGenreViewModel> vm = _mapper.Map<List<FavouriteGenreViewModel>>(favouriteGenreList);

            return vm;
        }

    }

    public class FavouriteGenreViewModel
    {
        public int CustomerId { get; set; }
        public int GenreId { get; set; }
        public bool IsActive { get; set; } = true;
    }
}