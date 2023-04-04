using FluentValidation;

namespace WebApi.Application.FavouriteGenreOperations.Commands.UpdateFavouriteGenre
{
    public class UpdateFavouriteGenreCommandValidator : AbstractValidator<UpdateFavouriteGenreCommand>
    {
        public UpdateFavouriteGenreCommandValidator()
        {
            RuleFor(command => command.Model.CustomerId).GreaterThan(0);
            RuleFor(command => command.Model.GenreId).GreaterThan(0);
        }
    }
}