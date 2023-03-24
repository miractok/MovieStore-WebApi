using FluentValidation;

namespace WebApi.Application.DirectorFilmOperations.Commands.UpdateDirectorFilm
{
    public class UpdateDirectorFilmCommandValidator : AbstractValidator<UpdateDirectorFilmCommand>
    {
        public UpdateDirectorFilmCommandValidator()
        {
            RuleFor(command => command.Model.FilmId).GreaterThan(0);
            RuleFor(command => command.Model.DirectorId).GreaterThan(0);
        }
    }
}