using AutoFixture.AutoMoq;
using Bunit;
using Carlton.Core.Components.Flux.Contracts;
using Carlton.Core.Components.Flux.Test.Common;
using Carlton.Core.Components.Flux.Test.Common.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Carlton.Core.Components.Flux.Test.ComponentTests;

public class ConnectedWrapperComponentTests : TestContext
{
    private readonly IFixture _fixture;
    private readonly Mock<IViewModelQueryDispatcher<TestState>> _vmDispatcher = new();
    private readonly Mock<IMutationCommandDispatcher<TestState>> _mutationDispatcher = new();
    private readonly Mock<IFluxStateObserver<TestState>> _observer = new();
    private readonly Mock<ILogger<ConnectedWrapper<TestViewModel1, TestState>>> _logger = new();


    public ConnectedWrapperComponentTests() 
    {
        //Arrange
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
        Services.AddSingleton<IConnectedComponent<TestViewModel1>>(new DummyConnectedComponent());
        Services.AddSingleton(_vmDispatcher.Object);
        Services.AddSingleton(_mutationDispatcher.Object);
        Services.AddSingleton(_observer.Object);
        Services.AddSingleton(_logger.Object);
    }

    [Fact]
    public void ConnectedWrapperRendersCorrectly()
    {
        //Arrange
        var vm = _fixture.Create<TestViewModel1>();
        var expectedMarkup = @$"
<div class=""vm-props"">
  <span class=""id"">{vm.ID}</span>
  <span class=""name"">{vm.Name}</span>
  <span class=""description"">{vm.Description}</span>
  <button>Command Event Test</button>
</div>";
        _vmDispatcher.SetupDispatcher(vm);

        // Act
        var cut = RenderComponent<ConnectedWrapper<TestViewModel1, TestState>>();

        // Assert
        cut.MarkupMatches(expectedMarkup);
    }

    [Fact]
    public void ConnectedWrapperInitializesCorrectly()
    {
        //Arrange
        var vm = _fixture.Create<TestViewModel1>();
        _vmDispatcher.SetupDispatcher(vm);

        // Act
        _ = RenderComponent<ConnectedWrapper<TestViewModel1, TestState>>();

        // Assert
        _vmDispatcher.VerifyDispatcher<TestViewModel1>(1);
    }


    [Fact]
    public void ConnectedWrapper_OnComponentEvent_CallsMutationDispatcher()
    {
        //Arrange
        var vm = _fixture.Create<TestViewModel1>();
        _vmDispatcher.SetupDispatcher(vm);

        var cut = RenderComponent<ConnectedWrapper<TestViewModel1, TestState>>();
        var buttonElement = cut.Find("button");

        // Act
        buttonElement.Click();

        // Assert
        _mutationDispatcher.VerifyDispatcher<TestCommand1>();
    }

    [Fact]
    public void ConnectedWrapper_ObservableStateEvents_InitializeCorrectly()
    {
        //Arrange
        var vm = _fixture.Create<TestViewModel1>();
        _vmDispatcher.SetupDispatcher(vm);

        // Act
        var cut = RenderComponent<ConnectedWrapper<TestViewModel1, TestState>>();

        // Assert
        Assert.Collection(cut.Instance.ObserveableStateEvents,
          evt => Assert.Equal("TestEvent", evt),
          evt => Assert.Equal("TestEvent2", evt),
          evt => Assert.Equal("TestEvent3", evt)
      );  
    }

    [Fact]
    public void ConnectedWrapper_OnStateChangeTestEvent_CallsViewModelDispatcher()
    {
        // Arrange
        var vm = _fixture.Create<TestViewModel1>();
        _vmDispatcher.SetupDispatcher(vm);

        RenderComponent<ConnectedWrapper<TestViewModel1, TestState>>();
        var times = 2; //Once for the component init and once again for the state change

        //Act
        _observer.Raise(_ => _.StateChanged += null, "TestEvent");

        // Assert
        _vmDispatcher.VerifyDispatcher<TestViewModel1>(times);
    }

    [Theory, AutoData]
    public void ConnectedWrapper_OnStateChangeNonListeningEvent_DoesNotCallViewModelDispatcher(string stateEventName)
    {
        // Arrange
        var vm = _fixture.Create<TestViewModel1>();
        _vmDispatcher.SetupDispatcher(vm);

        RenderComponent<ConnectedWrapper<TestViewModel1, TestState>>();
        var times = 1; //One and only time for the component init

        //Act
        _observer.Raise(_ => _.StateChanged += null, stateEventName);

        // Assert
        _vmDispatcher.VerifyDispatcher<TestViewModel1>(times);
    }

    [Fact]
    public void ConnectedWrapperDisposesCorrectly()
    {
        //Arrange
        var vm = _fixture.Create<TestViewModel1>();
        _vmDispatcher.SetupDispatcher(vm);

        RenderComponent<ConnectedWrapper<TestViewModel1, TestState>>();
        var times = 1; //One and only time for the component init, event handler removed correctly during dispose
        DisposeComponents();

        //Act
        _observer.Raise(_ => _.StateChanged += null, "TestEvent");

        // Assert
        _vmDispatcher.VerifyDispatcher<TestViewModel1>(times);
    }

    [Fact]
    public void ConnectedWrapper_LoadingContent()
    {
        //Arrange
        var vm = _fixture.Create<TestViewModel1>();
        _vmDispatcher.SetupDispatcher(vm);

        var spinnerMarkup = "<span class='spinner'>This is a spinner.</span>";
        var renderFragment = new RenderFragment(builder =>
        {
            builder.AddMarkupContent(0, spinnerMarkup);
        });

        var propInfo = typeof(ConnectedWrapper<TestViewModel1, TestState>)
            .GetProperty("IsLoading");

        var cut = RenderComponent<ConnectedWrapper<TestViewModel1, TestState>>(parameters => parameters
            .Add(p => p.SpinnerContent, renderFragment));

        //Act
        propInfo.SetValue(cut.Instance, true);
        cut.Render();

        //Assert
        cut.MarkupMatches(spinnerMarkup);
    }
}


