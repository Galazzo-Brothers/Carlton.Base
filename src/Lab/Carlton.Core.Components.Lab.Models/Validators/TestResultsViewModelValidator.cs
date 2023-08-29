namespace Carlton.Core.Components.Lab.Models.Validators;

public class TestResultsViewModelValidator : AbstractValidator<TestResultsViewModel>
{
    public TestResultsViewModelValidator() 
    {
        RuleFor(_ => _.TestResults).NotNull();
    }
}
