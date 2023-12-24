namespace Carlton.Core.Lab.Models.Validators.ViewModels;

public class EventConsoleViewModelValidator : AbstractValidator<EventConsoleViewModel>
{
    public EventConsoleViewModelValidator()
    {
        RuleFor(_ => _.RecordedEvents).NotNull();
    }
}
