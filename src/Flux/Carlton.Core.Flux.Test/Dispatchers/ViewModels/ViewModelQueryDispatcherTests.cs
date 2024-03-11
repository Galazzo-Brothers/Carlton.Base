using Carlton.Core.Foundation.Test;
using Carlton.Core.Components.Flux.Tests.Common.Extensions;
using Carlton.Core.Flux.Test.Common.Extensions;
using Carlton.Core.Flux.Dispatchers.ViewModels;
using Carlton.Core.Flux.Internals.Contracts;
using Carlton.Core.Flux.Internals.Dispatchers.ViewModels;
namespace Carlton.Core.Flux.Tests.Dispatchers.ViewModels;

public class ViewModelQueryDispatcherTests
{
	[Theory, AutoNSubstituteData]
	internal async Task Dispatch_ShouldReturnViewModel(
		[Frozen] IServiceProvider serviceProvider,
		[Frozen] IViewModelQueryHandler<TestState> handler,
		ViewModelQueryDispatcher<TestState> sut,
		object sender,
		ViewModelQueryContext<TestViewModel> context,
		TestViewModel expectedViewModel)
	{
		//Arrange
		serviceProvider.SetupServiceProvider<IViewModelQueryHandler<TestState>>(handler);
		handler.SetupHandler(expectedViewModel);

		//Act
		var result = await sut.Dispatch(sender, context, CancellationToken.None);

		//Assert
		handler.VerifyHandler<TestViewModel>();
		result.Match(vm => vm, err => throw new Exception()).ShouldBe(expectedViewModel);
	}
}



