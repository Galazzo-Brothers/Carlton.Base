
namespace Carlton.Core.Components.Lab.Models.Validators.Commands;

public class SelectMenuExpandedCommandValidator : AbstractValidator<SelectMenuExpandedCommand>
{
    public SelectMenuExpandedCommandValidator()
    {
        RuleFor(command => command.SelectedComponentIndex).GreaterThanOrEqualTo(0);
    }
}
