using Carlton.Core.Components.Lab.Models.Validators.Base;

namespace Carlton.Core.Components.Lab.Models.Validators.ViewModels;

public class NavMenuViewModelValidator : AbstractValidator<NavMenuViewModel>
{
    public NavMenuViewModelValidator()
    {
        RuleFor(vm => vm.MenuItems)
            .NotNull()
            .NotEmpty();
        //RuleForEach(vm => vm.MenuItems)
        //    .SetValidator(new ComponentStateValidator());
    }
}
