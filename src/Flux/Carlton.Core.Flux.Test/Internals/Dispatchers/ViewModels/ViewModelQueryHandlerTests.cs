using Carlton.Core.Flux.Internals.Dispatchers.ViewModels;
using NSubstitute.ExceptionExtensions;
using Carlton.Core.Foundation.Tests;
namespace Carlton.Core.Flux.Tests.Internals.Dispatchers.ViewModels;

public class ViewModelQueryHandlerTests
{
	[Theory, AutoNSubstituteData]
	internal async Task Handle_ShouldReturnViewModel(
		[Frozen] IViewModelMapper<TestState> mapper,
		[Frozen] IFluxState<TestState> fluxState,
		ViewModelQueryHandler<TestState> sut,
		TestState state,
		ViewModelQueryContext<TestViewModel> context,
		TestViewModel expectedViewModel)
	{
		//Arrange
		fluxState.CurrentState.Returns(state);
		mapper.Map<TestViewModel>(state).Returns(expectedViewModel);

		//Act
		var result = await sut.Handle(context, CancellationToken.None);

		//Assert
		mapper.Received().Map<TestViewModel>(state);
		result.ShouldBe(expectedViewModel);
	}

	[Theory, AutoNSubstituteData]
	internal async Task Handle_WithMappingError_ShouldReturnError(
		[Frozen] IViewModelMapper<TestState> mapper,
		[Frozen] IFluxState<TestState> fluxState,
		ViewModelQueryHandler<TestState> sut,
		TestState state,
		Exception ex,
		ViewModelQueryContext<TestViewModel> context)
	{
		//Arrange
		fluxState.CurrentState.Returns(state);
		mapper.Map<TestViewModel>(state).Throws(ex);

		//Act
		var result = await sut.Handle(context, CancellationToken.None);

		//Assert
		mapper.Received().Map<TestViewModel>(state);
		result.GetError().ShouldBeOfType<MappingError>();
	}
}
