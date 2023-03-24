using FluentValidation;

namespace WebApi.Application.ActorFilmOperations.Commands.DeleteActorFilm
{
    public class DeleteActorFilmCommandValidator : AbstractValidator<DeleteActorFilmCommand>
    {
        public DeleteActorFilmCommandValidator()
        {
            RuleFor(command => command.DataId).GreaterThan(0);
        }
    }
}