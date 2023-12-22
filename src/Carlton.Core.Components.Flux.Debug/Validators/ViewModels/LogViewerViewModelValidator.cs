using Carlton.Core.Components.Flux.Debug.ViewModels;
using FluentValidation;

namespace Carlton.Core.Components.Flux.Debug.Validators.ViewModels;

public class LogViewerViewModelValidator : AbstractValidator<LogViewerViewModel>
{
    public LogViewerViewModelValidator()
    {
        RuleFor(vm => vm.LogMessages).NotNull();
    }
}
