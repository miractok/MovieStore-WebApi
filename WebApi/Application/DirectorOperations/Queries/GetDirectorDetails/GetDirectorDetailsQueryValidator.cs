using FluentValidation;

namespace WebApi.Application.DirectorOperations.Queries.GetDirectorDetails
{
    public class GetDirectorDetailsQueryValidator : AbstractValidator<GetDirectorDetailsQuery>
    {
        public GetDirectorDetailsQueryValidator()
        {
            RuleFor(query => query.DirectorId).GreaterThan(0);
        }
    }
}