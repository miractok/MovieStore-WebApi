using FluentValidation;

namespace WebApi.Application.OrderOperations.Queries.GetOrderDetails
{
    public class GetOrderDetailQueryValidator : AbstractValidator<GetOrderDetailsQuery>
    {
        public GetOrderDetailQueryValidator()
        {
            RuleFor(query => query.OrderId).GreaterThan(0);
        }
    }
}