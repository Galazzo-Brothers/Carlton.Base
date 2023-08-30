namespace Carlton.Core.Components.Lab.Models.Validators.Base;

public class ComponentStateValidator : AbstractValidator<ComponentState>
{
    public ComponentStateValidator()
    {
        RuleFor(_ => _.DisplayName).NotNull().NotEmpty();
        RuleFor(_ => _.Type).NotNull();
        RuleFor(_ => _.ComponentParameters).NotNull().SetValidator(new ComponentParametersValidator());
    }
}
