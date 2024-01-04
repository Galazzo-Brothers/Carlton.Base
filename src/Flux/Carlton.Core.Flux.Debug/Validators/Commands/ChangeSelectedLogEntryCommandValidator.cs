namespace Carlton.Core.Flux.Debug.Validators.Commands;

public class ChangeSelectedLogEntryCommandValidator : AbstractValidator<ChangeSelectedLogEntryCommand>
{
    public ChangeSelectedLogEntryCommandValidator()
    {
        RuleFor(command => command.SelectedLogEntry).NotNull();
    }
}