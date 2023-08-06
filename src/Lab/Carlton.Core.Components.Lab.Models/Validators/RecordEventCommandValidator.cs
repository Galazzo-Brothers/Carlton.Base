namespace Carlton.Core.Components.Lab.Models;

public class RecordEventCommandValidator : AbstractValidator<RecordEventCommand>
{
    public RecordEventCommandValidator()
    {
        RuleFor(command => command.RecordedEventName).NotNull();
    }
}
