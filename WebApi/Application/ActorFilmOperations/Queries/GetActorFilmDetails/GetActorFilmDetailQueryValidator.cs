using FluentValidation;

namespace WebApi.Application.ActorFilmOperations.Queries.GetActorFilmDetails
{
    public class GetActorFilmDetailQueryValidator : AbstractValidator<GetActorFilmDetailQuery>
    {
        public GetActorFilmDetailQueryValidator()
        {
            RuleFor(query => query.ActorFilmId).GreaterThan(0);
        }
    }
}