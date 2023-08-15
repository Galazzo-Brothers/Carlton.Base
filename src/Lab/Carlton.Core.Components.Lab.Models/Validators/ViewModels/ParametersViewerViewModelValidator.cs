namespace Carlton.Core.Components.Lab.Models.Validators.ViewModels;

public class ParametersViewerViewModelValidator : AbstractValidator<ParametersViewerViewModel>
{
    public ParametersViewerViewModelValidator()
    {
        RuleFor(_ => _.ComponentParameters).NotNull().SetValidator(new ComponentParametersValidator());
    }
}
