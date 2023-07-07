namespace Carlton.Base.TestBed.Models;

public class UpdateParametersCommandValidator : AbstractValidator<UpdateParametersCommand>
{
    public UpdateParametersCommandValidator()
    {
        RuleFor(command => command.ComponentParameters).NotNull();
    }
}
