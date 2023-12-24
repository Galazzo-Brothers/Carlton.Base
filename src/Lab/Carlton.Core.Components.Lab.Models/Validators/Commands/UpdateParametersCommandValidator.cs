namespace Carlton.Core.Lab.Models.Validators.Commands;
public class UpdateParametersCommandValidator : AbstractValidator<UpdateParametersCommand>
{
    public UpdateParametersCommandValidator()
    {
        RuleFor(command => command.Parameters).NotNull();
    }
}
