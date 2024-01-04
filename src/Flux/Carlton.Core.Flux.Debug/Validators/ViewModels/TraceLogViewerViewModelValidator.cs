namespace Carlton.Core.Flux.Debug.Validators.ViewModels;

public class TraceLogViewerViewModelValidator : AbstractValidator<TraceLogViewerViewModel>
{
    public TraceLogViewerViewModelValidator()
    {
        RuleFor(vm => vm.LogEntries).NotNull();
    }
}
