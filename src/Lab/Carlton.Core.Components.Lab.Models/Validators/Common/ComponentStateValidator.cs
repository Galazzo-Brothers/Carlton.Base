namespace Carlton.Core.Lab.Models.Validators.Common;

public class ComponentStateValidator : AbstractValidator<ComponentState>
{
    public ComponentStateValidator()
    {
        RuleFor(_ => _.DisplayName).NotNull().NotEmpty();
        RuleFor(_ => _.ComponentParameters).NotNull().SetValidator(new ComponentParametersValidator());
    }
}
