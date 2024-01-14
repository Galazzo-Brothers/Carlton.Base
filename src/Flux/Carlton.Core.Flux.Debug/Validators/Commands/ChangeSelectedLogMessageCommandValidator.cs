namespace Carlton.Core.Flux.Debug.Validators.Commands;

public class ChangeSelectedLogMessageCommandValidator : AbstractValidator<ChangeSelectedLogMessageCommand>
{
    public ChangeSelectedLogMessageCommandValidator()
    {
        RuleFor(command => command.SelectedLogMessage).NotNull();
    }
}