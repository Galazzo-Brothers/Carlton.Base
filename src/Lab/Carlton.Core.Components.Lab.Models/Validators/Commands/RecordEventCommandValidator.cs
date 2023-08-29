namespace Carlton.Core.Components.Lab.Models.Validators.Commands;

public class RecordEventCommandValidator : AbstractValidator<RecordEventCommand>
{
    public RecordEventCommandValidator()
    {
        RuleFor(command => command.RecordedEventName).NotNull().NotEmpty();
    }
}
