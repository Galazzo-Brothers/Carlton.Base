using Carlton.Core.Flux.Components;
using Carlton.Core.Flux.Contracts;
using Carlton.Core.Flux.Dispatchers.Mutations;
using Carlton.Core.Flux.Dispatchers.ViewModels;
using Carlton.Core.Flux.Logging;
using Carlton.Core.Foundation.Test;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
namespace Carlton.Core.Flux.Tests.Components.FluxComponents;

public class FluxComponentTests : TestContext
{
    private readonly IViewModelQueryDispatcher<TestState> _mockQueryDispatcher;
    private readonly IMutationCommandDispatcher<TestState> _mockCommandDispatcher;
    private readonly IFluxStateObserver<TestState> _mockObserver;
    private readonly ILogger<FluxWrapper<TestState, TestViewModel>> _mockLogger;

    //Arrange
    public FluxComponentTests()
    {
        //Create Mocks
        _mockQueryDispatcher = Substitute.For<IViewModelQueryDispatcher<TestState>>();
        _mockCommandDispatcher = Substitute.For<IMutationCommandDispatcher<TestState>>();
        _mockObserver = Substitute.For<IFluxStateObserver<TestState>>();
        _mockLogger = Substitute.For<ILogger<FluxWrapper<TestState, TestViewModel>>>();

        //Container Registrations
        Services.AddSingleton<IConnectedComponent<TestViewModel>>(new DummyConnectedComponent());
        Services.AddSingleton(_mockQueryDispatcher);
        Services.AddSingleton(_mockCommandDispatcher);
        Services.AddSingleton(_mockObserver);
        Services.AddSingleton(_mockLogger);
    }

    [Theory, AutoData]
    public void FluxComponent_RendersCorrectly(TestViewModel vm)
    {
        //Arrange
        var expectedMarkup = @$"
            <div class=""vm-props"">
              <span class=""id"">{vm.ID}</span>
              <span class=""name"">{vm.Name}</span>
              <span class=""description"">{vm.Description}</span>
              <button>Command Event Test</button>
            </div>";

        _mockQueryDispatcher.SetupQueryDispatcher(vm);

        // Act
        var cut = RenderComponent<FluxComponent<TestState, TestViewModel>>();

        // Assert
        cut.MarkupMatches(expectedMarkup);
    }

    [Theory, AutoNSubstituteData]
    public void ConnectedWrapper_RendersErrorContentCorrectly(
        [Frozen] ViewModelQueryContext<TestViewModel> context,
        Exception exception)
    {
        //Arrange
        var expectedMarkupTemplate = @"
            <div class=""error-prompt"">
              <span class=""header"">{0}</span>
              <span class=""icon {1}""></span>
              <span class=""message"">{2}</span>
              <button>Command Event Test</button>
            </div>";
        var error = new TestError(context);
        _mockQueryDispatcher.SetupQueryDispatcherException(exception);
        var expectedMarkup = string.Format(expectedMarkupTemplate, "Error", "mdi-alert-circle-outline", FluxLogs.FriendlyErrorMsg);

        // Act
        var cut = RenderComponent<FluxComponent<TestState, TestViewModel>>(
            parameters => parameters.Add(
                p => p.ErrorPrompt, err => string.Format(expectedMarkupTemplate, err.Header, err.IconClass, err.Message)));

        // Assert  
        cut.MarkupMatches(expectedMarkup);
    }

    [Theory, AutoNSubstituteData]
    public void ConnectedWrapper_MutationError_RendersErrorContentCorrectly(
        TestViewModel vm,
        MutationCommandContext<TestCommand1> context)
    {
        //Arrange
        var expectedMarkupTemplate = @"
            <div class=""error-prompt"">
              <span class=""header"">{0}</span>
              <span class=""icon {1}""></span>
              <span class=""message"">{2}</span>
              <button>Command Event Test</button>
            </div>";


        var error = new TestError(context);
        _mockQueryDispatcher.SetupQueryDispatcher(vm);
        _mockCommandDispatcher.SetupCommandDispatcherError(error);
        var expectedMarkup = string.Format(expectedMarkupTemplate, "Error", "mdi-alert-circle-outline", error.Message);

        var cut = RenderComponent<FluxComponent<TestState, TestViewModel>>(
            parameters => parameters.Add(
                p => p.ErrorPrompt, err => string.Format(expectedMarkupTemplate, err.Header, err.IconClass, err.Message)));

        //Act
        cut.Find("button").Click();

        // Assert  
        cut.MarkupMatches(expectedMarkup);
    }
}
