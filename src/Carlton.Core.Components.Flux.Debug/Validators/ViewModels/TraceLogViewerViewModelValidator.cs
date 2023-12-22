using Carlton.Core.Components.Flux.Debug.ViewModels;
using FluentValidation;

namespace Carlton.Core.Components.Flux.Debug.Validators.ViewModels;

public class TraceLogViewerViewModelValidator : AbstractValidator<TraceLogViewerViewModel>
{
    public TraceLogViewerViewModelValidator()
    {
        RuleFor(vm => vm.LogMessages).NotNull();
    }
}
