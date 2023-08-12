using FluentValidation;

namespace WebApi.Application.FilmOperations.Commands.CreateFilm
{
    public class CreateFilmCommandValidator : AbstractValidator<CreateFilmCommand>
    {
        public CreateFilmCommandValidator()
        {
            RuleFor(command => command.Model.Title).NotEmpty().NotNull().MinimumLength(1).NotEqual("string");
            RuleFor(command => command.Model.PublishDate.Date).NotEmpty().LessThan(DateTime.Now.Date);
            RuleFor(command => command.Model.GenreId).GreaterThan(0);
            RuleFor(command => command.Model.Price).NotEmpty().GreaterThan(0);
        }
    }
}