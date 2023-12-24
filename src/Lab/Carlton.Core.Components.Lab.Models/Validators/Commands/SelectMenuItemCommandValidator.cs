

namespace Carlton.Core.Lab.Models.Validators.Commands;

public class SelectMenuItemCommandValidator : AbstractValidator<SelectMenuItemCommand>
{
    public SelectMenuItemCommandValidator()
    {
        RuleFor(command => command.ComponentIndex).GreaterThanOrEqualTo(0);
        RuleFor(command => command.ComponentStateIndex).GreaterThanOrEqualTo(0);
        RuleFor(command => command.SelectedComponentState).NotNull();
        RuleFor(command => command.SelectedComponentState).SetValidator(new ComponentStateValidator());
    }
}


