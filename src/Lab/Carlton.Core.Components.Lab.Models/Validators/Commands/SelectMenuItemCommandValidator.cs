using Carlton.Core.Components.Lab.Models.Validators.Base;

namespace Carlton.Core.Components.Lab.Models.Validators.Commands;

public class SelectMenuItemCommandValidator : AbstractValidator<SelectMenuItemCommand>
{
    public SelectMenuItemCommandValidator()
    {
        RuleFor(command => command.SelectedComponentState).NotNull();
        RuleFor(command => command.SelectedComponentState).SetValidator(new ComponentStateValidator());
    }
}


