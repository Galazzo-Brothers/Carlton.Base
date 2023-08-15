namespace Carlton.Core.Components.Lab.Models.Validators.ViewModels;

public class NavMenuViewModelValidator : AbstractValidator<NavMenuViewModel>
{
    public NavMenuViewModelValidator()
    {
        RuleFor(vm => vm.MenuItems).NotNull();
        RuleFor(vm => vm.SelectedItem).NotNull().SetValidator(new ComponentStateValidator());
    }
}
