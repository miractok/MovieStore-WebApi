using FluentValidation;

namespace WebApi.Application.ActorFilmOperations.Commands.UpdateActorFilm
{
    public class UpdateActorFilmCommandValidator : AbstractValidator<UpdateActorFilmCommand>
    {
        public UpdateActorFilmCommandValidator()
        {
            RuleFor(command => command.Model.FilmId).GreaterThan(0);
            RuleFor(command => command.Model.ActorId).GreaterThan(0);
        }
    }
}