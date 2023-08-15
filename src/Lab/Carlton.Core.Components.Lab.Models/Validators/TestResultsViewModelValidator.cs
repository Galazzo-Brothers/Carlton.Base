using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carlton.Core.Components.Lab.Models.Validators;

public class TestResultsViewModelValidator : AbstractValidator<TestResultsViewModel>
{
    public TestResultsViewModelValidator() 
    {
        RuleFor(_ => _.TestResultsReport).NotNull();
        RuleFor(_ => _.TestResultsReport.TestResults).NotNull();
    }
}
