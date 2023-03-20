using FluentValidation;

namespace WebApi.Application.DirectorOperations.Commands.UpdateDirector
{
    public class UpdateActorCommandValidator : AbstractValidator<UpdateDirectorCommand>
    {
        public UpdateActorCommandValidator()
        {
            RuleFor(command => command.DirectorId).GreaterThan(0);
            RuleFor(command => command.Model.NameSurname).MinimumLength(1).NotEmpty();
        }
    }
}