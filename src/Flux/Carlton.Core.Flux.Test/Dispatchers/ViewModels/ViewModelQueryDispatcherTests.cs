using Carlton.Core.Flux.Contracts;
using Carlton.Core.Foundation.Test;
using Carlton.Core.Components.Flux.Tests.Common.Extensions;
using Carlton.Core.Flux.Test.Common.Extensions;
using Carlton.Core.Flux.Dispatchers.ViewModels;
namespace Carlton.Core.Flux.Tests.Dispatchers.ViewModels;

public class ViewModelQueryDispatcherTests
{
	[Theory, AutoNSubstituteData]
	public async Task Dispatch_ShouldReturnViewModel(
		[Frozen] IServiceProvider serviceProvider,
		[Frozen] IViewModelQueryHandler<TestState> handler,
		ViewModelQueryDispatcher<TestState> sut,
		object sender,
		ViewModelQueryContext<TestViewModel> queryContext,
		TestViewModel expectedViewModel)
	{
		//Arrange
		serviceProvider.SetupServiceProvider<IViewModelQueryHandler<TestState>>(handler);
		handler.SetupHandler(expectedViewModel);

		//Act
		var result = await sut.Dispatch(sender, queryContext, CancellationToken.None);

		//Assert
		handler.VerifyHandler<TestViewModel>();
		result.Match(vm => vm, err => throw new Exception()).ShouldBe(expectedViewModel);
	}
}



