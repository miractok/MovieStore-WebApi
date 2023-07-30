using FluentValidation;

namespace WebApi.Application.ActorOperations.Queries.GetActorDetails
{
    public class GetActorDetailsQueryValidator : AbstractValidator<GetActorDetailsQuery>
    {
        public GetActorDetailsQueryValidator()
        {
            RuleFor(query => query.ActorId).GreaterThan(0);
        }
    }
}