using FluentValidation;

namespace WebApi.Application.ActorFilmOperations.Commands.CreateActorFilm
{
    public class CreateActorFilmCommandValidator : AbstractValidator<CreateActorFilmCommand>
    {
        public CreateActorFilmCommandValidator()
        {
            RuleFor(command => command.Model.ActorId).GreaterThan(0);
            RuleFor(command => command.Model.FilmId).GreaterThan(0);
        }
    }
}