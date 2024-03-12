using Carlton.Core.Flux.Components;
using Carlton.Core.Foundation.Tests;
using Carlton.Core.Foundation.Tests.Common;
namespace Carlton.Core.Flux.Tests.Components.FluxComponents;

public class FluxComponentTests : TestContext
{
	[Fact]
	public void FluxComponent_RendersCorrectly()
	{
		//Arrange
		var happyPathMarkup = "<div>Happy Path</div>";
		ComponentFactories.AddStub<FluxWrapper<TestState, TestViewModel>>(happyPathMarkup);

		// Act
		var cut = RenderComponent<FluxComponent<TestState, TestViewModel>>();

		// Assert
		cut.MarkupMatches(happyPathMarkup);
	}

	[Fact]
	public void FluxComponent_Parameters_RendersCorrectly()
	{
		//Arrange
		var happyPathMarkup = "<div>Happy Path</div>";
		var expectedSpinnerContent = "<div>spinner</div>";
		ComponentFactories.AddStub<FluxWrapper<TestState, TestViewModel>>(happyPathMarkup);

		// Act
		var cut = RenderComponent<FluxComponent<TestState, TestViewModel>>(
			parameters => parameters.Add(p => p.SpinnerContent, expectedSpinnerContent));

		var stub = cut.FindComponent<Stub<FluxWrapper<TestState, TestViewModel>>>();

		// Assert
		stub.Instance.Parameters.Get(p => p.SpinnerContent).ShouldNotBeNull();
	}

	[Fact]
	public void FluxComponent_SpinnerContentParameter_RendersCorrectly()
	{
		//Arrange
		var happyPathMarkup = "<div>Happy Path</div>";
		var expectedSpinnerContent = "<div>spinner</div>";
		ComponentFactories.AddStub<FluxWrapper<TestState, TestViewModel>>(happyPathMarkup);

		// Act
		var cut = RenderComponent<FluxComponent<TestState, TestViewModel>>(
			parameters => parameters.Add(p => p.SpinnerContent, expectedSpinnerContent));

		var stub = cut.FindComponent<Stub<FluxWrapper<TestState, TestViewModel>>>();

		var fragment = stub.Instance.Parameters.Get(p => p.SpinnerContent);
		var cut2 = RenderComponent<RenderFragmentWrapper>(parameters => parameters
			.Add(p => p.TestFragment, fragment));

		// Assert
		cut2.Find(RenderFragmentWrapper.Render_Fragment_Wrapper_Class).InnerHtml.ShouldBe(expectedSpinnerContent);
	}

	[Theory, AutoNSubstituteData]
	public void ConnectedWrapper_ErrorContent_RendersCorrectly(
		Exception exception)
	{
		//Arrange
		ComponentFactories.AddStub<FluxWrapper<TestState, TestViewModel>>("");
		var expectedMarkupTemplate = @"
            <div class=""error-prompt"">
              <span class=""header"">{0}</span>
              <span class=""icon {1}""></span>
              <span class=""message"">{2}</span>
              <button>Command Event Test</button>
            </div>";


		var error = new TestError();
		var expectedMarkup = string.Format(expectedMarkupTemplate, "Error", "mdi-alert-circle-outline", "Oops! We are sorry an error has occurred. Please try again.");

		var cut = RenderComponent<FluxComponent<TestState, TestViewModel>>(
			parameters => parameters.Add(
				p => p.ErrorPrompt, err => string.Format(expectedMarkupTemplate, err.Header, err.IconClass, err.Message)));

		//Act
		ErrorBoundaryTestingUtility.SimulateException(cut.FindComponent<FluxErrorBoundary>().Instance, exception);
		cut.Render();

		// Assert  
		cut.MarkupMatches(expectedMarkup);
	}
}
