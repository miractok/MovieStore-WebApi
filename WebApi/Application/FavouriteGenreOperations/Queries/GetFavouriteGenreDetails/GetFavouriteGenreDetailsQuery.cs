using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;

namespace WebApi.Application.FavouriteGenreOperations.Queries.GetFavouriteGenreDetails
{
    public class GetFavouriteGenreDetailsQuery
    {
        private readonly IMovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public int favouriteGenreId { get; set; }
        public GetFavouriteGenreDetailsQuery(IMovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public FavouriteGenreViewIdModel Handle()
        {
            var favouriteGenre = _context.FavouriteGenres.Include(x=> x.Customer).Include(x=> x.Genre).Where(favouriteGenre=> favouriteGenre.Id == favouriteGenreId).SingleOrDefault();

            if(favouriteGenre == null)
                throw new InvalidOperationException("The Id you entered does not match any FavouriteGenre relation.");

            FavouriteGenreViewIdModel vm = _mapper.Map<FavouriteGenreViewIdModel>(favouriteGenre); 
            
            return vm;
        }
    }

    public class FavouriteGenreViewIdModel
    {
        public int CustomerId { get; set; }
        public int GenreId { get; set; }
        public bool IsActive { get; set; } = true;
    }
}