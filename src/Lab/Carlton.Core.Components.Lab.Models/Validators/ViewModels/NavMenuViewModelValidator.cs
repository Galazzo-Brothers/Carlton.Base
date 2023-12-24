namespace Carlton.Core.Lab.Models.Validators.ViewModels;

public class NavMenuViewModelValidator : AbstractValidator<NavMenuViewModel>
{
    public NavMenuViewModelValidator()
    {
        RuleFor(vm => vm.SelectedComponentIndex).GreaterThanOrEqualTo(0);
        RuleFor(vm => vm.SelectedStateIndex).GreaterThanOrEqualTo(0);
        RuleFor(vm => vm.MenuItems)
            .NotNull()
            .NotEmpty();
        RuleForEach(vm => vm.MenuItems)
            .SetValidator(new ComponentAvailableStateValidators());
    }
}
