using FluentValidation;

namespace WebApi.Application.DirectorFilmOperations.Queries.GetDirectorFilmDetails
{
    public class GetDirectorFilmDetailsQueryValidator : AbstractValidator<GetDirectorFilmDetailsQuery>
    {
        public GetDirectorFilmDetailsQueryValidator()
        {
            RuleFor(query => query.DirectorFilmId).GreaterThan(0);
        }
    }
}