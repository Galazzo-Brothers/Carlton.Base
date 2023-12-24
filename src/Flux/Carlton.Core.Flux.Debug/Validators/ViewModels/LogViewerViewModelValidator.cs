namespace Carlton.Core.Flux.Debug.Validators.ViewModels;

public class LogViewerViewModelValidator : AbstractValidator<LogViewerViewModel>
{
    public LogViewerViewModelValidator()
    {
        RuleFor(vm => vm.LogMessages).NotNull();
    }
}
