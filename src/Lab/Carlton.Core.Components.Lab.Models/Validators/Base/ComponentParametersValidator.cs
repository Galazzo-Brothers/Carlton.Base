namespace Carlton.Core.Components.Lab.Models.Validators.Base;

public class ComponentParametersValidator : AbstractValidator<ComponentParameters>
{
    public ComponentParametersValidator()
    {
        RuleFor(_ => _.ParameterObj).NotNull();
    }
}
