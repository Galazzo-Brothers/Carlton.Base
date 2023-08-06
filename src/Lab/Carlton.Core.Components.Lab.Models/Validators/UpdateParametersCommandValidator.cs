namespace Carlton.Core.Components.Lab.Models;
public class UpdateParametersCommandValidator : AbstractValidator<UpdateParametersCommand>
{
    public UpdateParametersCommandValidator()
    {
        RuleFor(command => command.ComponentParameters).NotNull();
    }
}
