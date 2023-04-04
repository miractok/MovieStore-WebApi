using FluentValidation;

namespace WebApi.Application.FavouriteGenreOperations.Commands.DeleteFavouriteGenre
{
    public class DeleteFavouriteGenreCommandValidator : AbstractValidator<DeleteFavouriteGenreCommand>
    {
        public DeleteFavouriteGenreCommandValidator()
        {
            RuleFor(command => command.DataId).GreaterThan(0);
        }
    }
}