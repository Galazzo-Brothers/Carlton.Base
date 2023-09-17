namespace Carlton.Core.Components.Lab.Models.Validators.Base;

public class ComponentAvailableStateValidators : AbstractValidator<ComponentAvailableStates>
{
    public ComponentAvailableStateValidators()
    {
        RuleFor(_ => _.ComponentType).NotNull();
        RuleForEach(_ => _.ComponentStates).NotNull().SetValidator(new ComponentStateValidator());
    }
}
