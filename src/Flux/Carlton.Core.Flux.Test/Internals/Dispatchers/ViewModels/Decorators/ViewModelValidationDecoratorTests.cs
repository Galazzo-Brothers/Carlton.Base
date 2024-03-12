using Carlton.Core.Foundation.Tests;
namespace Carlton.Core.Flux.Tests.Internals.Dispatchers.ViewModels.Decorators;

public class ViewModelValidationDecoratorTests : TestContext
{
	[Theory, AutoNSubstituteData]
	internal async Task ValidationDecoratorDispatch_DispatchCalled_ReturnsViewModel(
	   [Frozen] IViewModelQueryDispatcher<TestState> decorated,
	   ViewModelValidationDecorator<TestState> sut,
	   object sender,
	   ViewModelQueryContext<TestViewModel> context,
	   TestViewModel expectedResult)
	{
		//Arrange
		decorated.SetupQueryDispatcher(expectedResult);

		//Act 
		var actualResult = await sut.Dispatch(sender, context, CancellationToken.None);

		//Assert
		decorated.VerifyQueryDispatcher<TestViewModel>(1);
		actualResult.IsSuccess.ShouldBeTrue();
		actualResult.ShouldBe(expectedResult);
		context.RequestValidated.ShouldBeTrue();
		context.ValidationPassed.ShouldBeTrue();
	}

	[Theory, AutoNSubstituteData]
	internal async Task ValidationDecoratorDispatch_DispatchCalled_WithFailedValidation_ReturnsValidationError(
	  [Frozen] IViewModelQueryDispatcher<TestState> decorated,
	  ViewModelValidationDecorator<TestState> sut,
	  object sender,
	  ViewModelQueryContext<TestViewModel> context)
	{
		//Arrange
		var invalidViewModel = new TestViewModel(-1, "Testing", "This should fail");
		decorated.SetupQueryDispatcher(invalidViewModel);

		//Act 
		var actualResult = await sut.Dispatch(sender, context, CancellationToken.None);

		//Assert
		decorated.VerifyQueryDispatcher<TestViewModel>(1);
		context.RequestValidated.ShouldBeTrue();
		context.ValidationPassed.ShouldBeFalse();
		context.ValidationErrors.ShouldNotBeEmpty();
		actualResult.GetError().ShouldBeOfType<ValidationError>();
	}
}
