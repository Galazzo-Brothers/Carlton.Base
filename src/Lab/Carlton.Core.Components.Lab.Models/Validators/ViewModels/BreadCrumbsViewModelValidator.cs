namespace Carlton.Core.Components.Lab.Models.Validators.ViewModels;

public class BreadCrumbsViewModelValidator : AbstractValidator<BreadCrumbsViewModel>
{
    public BreadCrumbsViewModelValidator()
    {
        RuleFor(vm => vm.SelectedComponentState).NotNull().SetValidator(new ComponentStateValidator());
    }
}
