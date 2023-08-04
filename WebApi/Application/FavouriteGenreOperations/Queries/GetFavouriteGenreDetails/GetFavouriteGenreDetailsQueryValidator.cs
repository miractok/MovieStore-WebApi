using FluentValidation;

namespace WebApi.Application.FavouriteGenreOperations.Queries.GetFavouriteGenreDetails
{
    public class GetFavouriteGenreDetailsQueryValidator : AbstractValidator<GetFavouriteGenreDetailsQuery>
    {
        public GetFavouriteGenreDetailsQueryValidator()
        {
            RuleFor(query => query.favouriteGenreId).GreaterThan(0);
        }
    }
}