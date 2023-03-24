using FluentValidation;

namespace WebApi.Application.FilmOperations.Queries.GetFilmDetails
{
    public class GetFilmDetailQueryValidator : AbstractValidator<GetFilmDetailQuery>
    {
        public GetFilmDetailQueryValidator()
        {
            RuleFor(query => query.FilmId).GreaterThan(0);
        }
    }
}