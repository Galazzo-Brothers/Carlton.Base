using Carlton.Core.Flux.Contracts;

namespace Carlton.Core.Flux.Debug.Tests.Common;

public class TestState
{
}

internal record TestCommand1();

internal record TestCommand2();

internal record TestCommand3();

internal record TestViewModel1();

internal record TestViewModel2();

internal record TestViewModel3();

internal record TestStateViewModelProjectionMapper : IViewModelProjectionMapper<TestState>
{
	public TViewModel Map<TViewModel>(TestState state)
	{
		Type viewModelType = typeof(TViewModel);

		if (viewModelType == typeof(TestViewModel1))
		{
			return (dynamic)new TestViewModel1();
		}

		if (viewModelType == typeof(TestViewModel2))
		{
			return (dynamic)new TestViewModel2();
		}

		if (viewModelType == typeof(TestViewModel3))
		{
			return (dynamic)new TestViewModel3();
		}

		throw new Exception();
	}
}

