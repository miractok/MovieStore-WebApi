using FluentValidation;

namespace WebApi.Application.FavouriteGenreOperations.Commands.CreateFavouriteGenre
{
    public class CreateFavouriteGenreCommandValidator : AbstractValidator<CreateFavouriteGenreCommand>
    {
        public CreateFavouriteGenreCommandValidator()
        {
            RuleFor(command => command.Model.CustomerId).GreaterThan(0);
            RuleFor(command => command.Model.GenreId).GreaterThan(0);
        }
    }
}