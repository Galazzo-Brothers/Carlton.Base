namespace Carlton.Core.Components.Lab.Models.Validators.ViewModels;

public class ComponentParametersValidator : AbstractValidator<ComponentParameters>
{
    public ComponentParametersValidator()
    {
        RuleFor(_ => _.ParameterObj).NotNull();
    }
}
