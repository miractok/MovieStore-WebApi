using FluentValidation;

namespace WebApi.Application.DirectorOperations.Commands.UpdateDirector
{
    public class UpdateDirectorCommandValidator : AbstractValidator<UpdateDirectorCommand>
    {
        public UpdateDirectorCommandValidator()
        {
            RuleFor(command => command.DirectorId).GreaterThan(0);
            RuleFor(command => command.Model.NameSurname).MinimumLength(1).NotEmpty();
        }
    }
}