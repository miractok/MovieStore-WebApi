using FluentValidation;

namespace WebApi.Application.FavouriteGenreOperations.Queries.GetFavouriteGenreDetails
{
    public class GetFavouriteGenreDetailQueryValidator : AbstractValidator<GetFavouriteGenreDetailQuery>
    {
        public GetFavouriteGenreDetailQueryValidator()
        {
            RuleFor(query => query.favouriteGenreId).GreaterThan(0);
        }
    }
}