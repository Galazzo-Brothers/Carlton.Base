namespace Carlton.Core.Components.Lab.Models.Validators.ViewModels;

public class SourceViewerViewModelValidator : AbstractValidator<SourceViewerViewModel>
{
    public SourceViewerViewModelValidator()
    {
        RuleFor(_ => _.ComponentSource).NotNull().NotEmpty();
    }
}
