namespace Carlton.Base.TestBed.Models;

public class RecordEventCommandValidator : AbstractValidator<RecordEventCommand>
{
    public RecordEventCommandValidator()
    {
        RuleFor(command => command.RecordedEventName).NotNull();
    }
}
