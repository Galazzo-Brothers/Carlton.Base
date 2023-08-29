namespace Carlton.Core.Components.Lab.Models.Validators.Commands;

public class SourceViewerViewModelRefreshValidator : AbstractValidator<SourceViewerViewModel>
{
    public SourceViewerViewModelRefreshValidator()
    {
        RuleFor(_ => _.ComponentSource).NotNull().NotEmpty();
    }
}
