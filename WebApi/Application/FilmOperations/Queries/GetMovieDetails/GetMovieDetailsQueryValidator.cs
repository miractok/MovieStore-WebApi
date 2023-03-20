using FluentValidation;

namespace WebApi.Application.FilmOperations.Queries.GetMovieDetails
{
    public class GetMovieDetailQueryValidator : AbstractValidator<GetMovieDetailQuery>
    {
        public GetMovieDetailQueryValidator()
        {
            RuleFor(query => query.FilmId).GreaterThan(0);
        }
    }
}