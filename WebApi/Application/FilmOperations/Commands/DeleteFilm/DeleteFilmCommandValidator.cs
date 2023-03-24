using FluentValidation;

namespace WebApi.Application.FilmOperations.Commands.DeleteFilm
{
    public class DeleteFilmCommandValidator : AbstractValidator<DeleteFilmCommand>
    {
        public DeleteFilmCommandValidator()
        {
            RuleFor(command => command.FilmId).GreaterThan(0);
        }
    }
}