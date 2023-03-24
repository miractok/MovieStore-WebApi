using FluentValidation;

namespace WebApi.Application.DirectorFilmOperations.Queries.GetDirectorFilmDetails
{
    public class GetDirectorFilmDetailQueryValidator : AbstractValidator<GetDirectorFilmDetailQuery>
    {
        public GetDirectorFilmDetailQueryValidator()
        {
            RuleFor(query => query.DirectorFilmId).GreaterThan(0);
        }
    }
}