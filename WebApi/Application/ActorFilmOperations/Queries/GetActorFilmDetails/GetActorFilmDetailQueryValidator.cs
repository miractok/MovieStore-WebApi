using FluentValidation;

namespace WebApi.Application.ActorFilmOperations.Queries.GetActorFilmDetails
{
    public class GetActorFilmDetailsQueryValidator : AbstractValidator<GetActorFilmDetailsQuery>
    {
        public GetActorFilmDetailsQueryValidator()
        {
            RuleFor(query => query.ActorFilmId).GreaterThan(0);
        }
    }
}