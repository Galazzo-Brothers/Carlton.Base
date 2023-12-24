namespace Carlton.Core.Lab.Models.Validators.Common;

public class ComponentAvailableStateValidators : AbstractValidator<ComponentAvailableStates>
{
    public ComponentAvailableStateValidators()
    {
        RuleFor(_ => _.ComponentType).NotNull();
        RuleForEach(_ => _.ComponentStates).NotNull().SetValidator(new ComponentStateValidator());
    }
}
