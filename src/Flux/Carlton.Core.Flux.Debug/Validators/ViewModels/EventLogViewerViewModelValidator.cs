namespace Carlton.Core.Flux.Debug.Validators.ViewModels;

public class EventLogViewerViewModelValidator : AbstractValidator<EventLogViewerViewModel>
{
    public EventLogViewerViewModelValidator()
    {
        RuleFor(vm => vm.LogMessages).NotNull();
    }
}
