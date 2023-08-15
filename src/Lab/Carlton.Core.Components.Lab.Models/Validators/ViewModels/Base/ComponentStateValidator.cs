
namespace Carlton.Core.Components.Lab.Models.Validators.ViewModels;

public class ComponentStateValidator : AbstractValidator<ComponentState>
{
    public ComponentStateValidator()
    {
        RuleFor(_ => _.DisplayName).NotNull().NotEmpty();
        RuleFor(_ => _.Type).NotNull().NotEmpty();
        RuleFor(_ => _.ComponentParameters).NotNull().SetValidator(new ComponentParametersValidator());
    }
}
