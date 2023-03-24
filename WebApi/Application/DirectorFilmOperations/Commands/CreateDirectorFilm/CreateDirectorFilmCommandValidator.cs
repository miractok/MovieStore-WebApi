using FluentValidation;

namespace WebApi.Application.DirectorFilmOperations.Commands.CreateDirectorFilm
{
    public class CreateDirectorFilmCommandValidator : AbstractValidator<CreateDirectorFilmCommand>
    {
        public CreateDirectorFilmCommandValidator()
        {
            RuleFor(command => command.Model.FilmId).GreaterThan(0);
            RuleFor(command => command.Model.DirectorId).GreaterThan(0);
        }
    }
}