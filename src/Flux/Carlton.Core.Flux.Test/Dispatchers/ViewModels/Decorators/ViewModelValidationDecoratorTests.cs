using Carlton.Core.Flux.Contracts;
using Carlton.Core.Flux.Dispatchers.ViewModels.Decorators;
using Carlton.Core.Flux.Dispatchers.ViewModels;
using Carlton.Core.Foundation.Test;
using Microsoft.Extensions.Logging;
using Carlton.Core.Flux.Logging;
namespace Carlton.Core.Flux.Tests.Dispatchers.ViewModels.Decorators;

public class ViewModelValidationDecoratorTests : TestContext
{
    [Theory, AutoNSubstituteData]
    public async Task ValidationDecoratorDispatch_DispatchCalled_ReturnsViewModel(
       [Frozen] IViewModelQueryDispatcher<TestState> decorated,
       [Frozen] ILogger<ViewModelValidationDecorator<TestState>> logger,
       ViewModelValidationDecorator<TestState> sut,
       object sender,
       ViewModelQueryContext<TestViewModel> queryContext,
       TestViewModel expectedResult)
    {
        //Arrange
        decorated.SetupQueryDispatcher(expectedResult);

        //Act 
        var actualResult = await sut.Dispatch(sender, queryContext, CancellationToken.None);

        //Assert
        decorated.VerifyQueryDispatcher<TestViewModel>(1);
        actualResult.IsSuccess.ShouldBeTrue();
        actualResult.ShouldBe(expectedResult);
        queryContext.RequestValidated.ShouldBeTrue();
        queryContext.ValidationResult.ValidationPassed.ShouldBeTrue();
    }

    [Theory, AutoNSubstituteData]
    public async Task ValidationDecoratorDispatch_DispatchCalled_WithFailedValidation_ReturnsValidationError(
      [Frozen] IViewModelQueryDispatcher<TestState> decorated,
      [Frozen] ILogger<ViewModelValidationDecorator<TestState>> logger,
      ViewModelValidationDecorator<TestState> sut,
      object sender,
      ViewModelQueryContext<TestViewModel> queryContext)
    {
        //Arrange
        var invalidViewModel = new TestViewModel(-1, "Testing", "This should fail");
        decorated.SetupQueryDispatcher(invalidViewModel);

        //Act 
        var actualResult = await sut.Dispatch(sender, queryContext, CancellationToken.None);

        //Assert
        decorated.VerifyQueryDispatcher<TestViewModel>(1);
        queryContext.RequestValidated.ShouldBeTrue();
        queryContext.ValidationResult.ValidationErrors.ShouldNotBeEmpty();
        actualResult.GetError().ShouldBeOfType<ValidationError>();
        logger.Received().ViewModelQueryValidationFailure(queryContext.FluxOperationTypeName);
    }
}
