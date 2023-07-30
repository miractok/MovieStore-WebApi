using FluentValidation;

namespace WebApi.Application.FilmOperations.Queries.GetFilmDetails
{
    public class GetFilmDetailsQueryValidator : AbstractValidator<GetFilmDetailsQuery>
    {
        public GetFilmDetailsQueryValidator()
        {
            RuleFor(query => query.FilmId).GreaterThan(0);
        }
    }
}