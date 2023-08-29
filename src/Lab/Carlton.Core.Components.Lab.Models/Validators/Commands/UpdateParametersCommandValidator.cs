namespace Carlton.Core.Components.Lab.Models.Validators.Commands;
public class UpdateParametersCommandValidator : AbstractValidator<UpdateParametersCommand>
{
    public UpdateParametersCommandValidator()
    {
        RuleFor(command => command.Parameters).NotNull();
    }
}
