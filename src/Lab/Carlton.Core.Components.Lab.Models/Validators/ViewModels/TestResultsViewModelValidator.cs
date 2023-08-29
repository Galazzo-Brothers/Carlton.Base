namespace Carlton.Core.Components.Lab.Models.Validators.ViewModels;

public class TestResultsViewModelValidator : AbstractValidator<TestResultsViewModel>
{
    public TestResultsViewModelValidator()
    {
        RuleFor(_ => _.TestResults).NotNull();
    }
}
