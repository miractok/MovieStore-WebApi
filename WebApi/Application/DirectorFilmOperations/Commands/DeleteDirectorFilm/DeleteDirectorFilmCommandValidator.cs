using FluentValidation;

namespace WebApi.Application.DirectorFilmOperations.Commands.DeleteDirectorFilm
{
    public class DeleteDirectorFilmCommandValidator : AbstractValidator<DeleteDirectorFilmCommand>
    {
        public DeleteDirectorFilmCommandValidator()
        {
            RuleFor(command => command.DataId).GreaterThan(0);
        }
    }
}