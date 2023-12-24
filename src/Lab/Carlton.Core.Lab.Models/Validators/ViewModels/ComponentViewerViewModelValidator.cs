

namespace Carlton.Core.Lab.Models.Validators.ViewModels;

public class ComponentViewerViewModelValidator : AbstractValidator<ComponentViewerViewModel>
{
    public ComponentViewerViewModelValidator()
    {
        RuleFor(_ => _.ComponentType).NotNull();
        RuleFor(_ => _.ComponentParameters).NotNull().SetValidator(new ComponentParametersValidator());
    }
}
