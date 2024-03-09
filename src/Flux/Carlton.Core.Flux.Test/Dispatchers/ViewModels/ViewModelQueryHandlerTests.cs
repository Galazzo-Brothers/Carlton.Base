using Carlton.Core.Flux.Contracts;
using Carlton.Core.Flux.Dispatchers.ViewModels;
using Carlton.Core.Foundation.Test;
namespace Carlton.Core.Flux.Tests.Dispatchers.ViewModels;

public class ViewModelQueryHandlerTests
{
    [Theory, AutoNSubstituteData]
    public async Task Handle_ShouldCallStateMutation(
       [Frozen] IFluxState<TestState> fluxState,
       [Frozen] IViewModelMapper<TestState> mapper,
       ViewModelQueryHandler<TestState> sut,
       ViewModelQueryContext<TestViewModel> context,
       TestViewModel expectedViewModel)
    {
        //Arrange
        mapper.Map<TestViewModel>(fluxState.CurrentState).Returns(expectedViewModel);

        //Act
        var result = await sut.Handle(context, CancellationToken.None);

        //Assert
        mapper.Received().Map<TestViewModel>(fluxState.CurrentState);
        result.ShouldBe(expectedViewModel);
    }
}
