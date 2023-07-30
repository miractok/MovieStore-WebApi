using FluentValidation;

namespace WebApi.Application.FilmOperations.Commands.UpdateFilm
{
    public class UpdateFilmCommandValidator : AbstractValidator<UpdateFilmCommand>
    {
        public UpdateFilmCommandValidator()
        {
            RuleFor(command => command.FilmId).GreaterThan(0);
            RuleFor(command => command.Model.Title).NotEmpty().MinimumLength(1);
            RuleFor(command => command.Model.PublishDate.Date).NotEmpty().LessThan(DateTime.Now.Date);
            RuleFor(command => command.Model.GenreId).GreaterThan(0);
            RuleFor(command => command.Model.Price).NotEmpty().GreaterThan(0);
        }
    }
}