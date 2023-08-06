namespace Carlton.Core.Components.Lab.Models;

public class SelectMenuItemCommandValidator : AbstractValidator<SelectMenuItemCommand>
{
    public SelectMenuItemCommandValidator()
    {
        RuleFor(command => command.ComponentState).NotNull();
        RuleFor(command => command.ComponentState.DisplayName).NotNull().NotEmpty();
        RuleFor(command => command.ComponentState.Type).NotNull();
        RuleFor(command => command.ComponentState.ComponentParameters).NotNull();
    }
}
