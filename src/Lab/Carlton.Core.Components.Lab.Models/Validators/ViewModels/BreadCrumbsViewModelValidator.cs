namespace Carlton.Core.Lab.Models.Validators.ViewModels;

public class BreadCrumbsViewModelValidator : AbstractValidator<BreadCrumbsViewModel>
{
    public BreadCrumbsViewModelValidator()
    {
        RuleFor(vm => vm.SelectedComponent).NotNull().NotEmpty();
        RuleFor(vm => vm.SelectedComponentState).NotNull().NotEmpty();
    }
}
