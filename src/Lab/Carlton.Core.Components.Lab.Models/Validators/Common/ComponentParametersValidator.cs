namespace Carlton.Core.Lab.Models.Validators.Common;

public class ComponentParametersValidator : AbstractValidator<ComponentParameters>
{
    public ComponentParametersValidator()
    {
        RuleFor(_ => _.ParameterObj).NotNull();
    }
}
