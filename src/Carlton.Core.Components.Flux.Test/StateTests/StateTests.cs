using Carlton.Core.Components.Flux.Contracts;
using Carlton.Core.Components.Flux.State;
using Carlton.Core.Components.Flux.Test.Common;
using MapsterMapper;
using Microsoft.Extensions.Logging;
using Moq;

namespace Carlton.Core.Components.Flux.Test.StateTests;

public class StateTests
{
    private readonly TestState _state = new();
    private readonly Mock<IServiceProvider> _serviceProvider = new();
    private readonly MutationResolver<TestState> _mutationResolver;
    private readonly Mock<IMapper> _mapper = new();
    private readonly Mock<ILogger<FluxState<TestState>>> _logger = new();
    private readonly FluxState<TestState> _fluxState;
    private readonly Mock<IFluxStateMutation<TestState, TestCommand1>> _mutation = new();
    private readonly TestCommand1 _command = new(new object(), 2, "test command", "this is a test");

    public StateTests()
    {
        _mutationResolver = new MutationResolver<TestState>(_serviceProvider.Object);
        _fluxState = new FluxState<TestState>(_state, _mutationResolver, _mapper.Object, _logger.Object);
        _mutation.Setup(_ => _.StateEvent).Returns("TestStateEvent");
        _serviceProvider.Setup(_ => _.GetService(typeof(IFluxStateMutation<TestState, TestCommand1>))).Returns(_mutation.Object);
    }

    [Fact]
    public async Task FluxState_MutateState_CallsMutation()
    {
        //Act
        await _fluxState.MutateState(_command);

        //Assert
        _mutation.Verify(_ => _.Mutate(_state, _command));
    }

    [Fact]
    public async Task FluxState_MutateState_RecordsStateEvents()
    {
        //Arrange
        _mutation.Setup(_ => _.StateEvent).Returns("TestStateEvent");
        _serviceProvider.Setup(_ => _.GetService(typeof(IFluxStateMutation<TestState, TestCommand1>))).Returns(_mutation.Object);

        //Act
        await _fluxState.MutateState(_command);

        //Assert
        Assert.NotEmpty(_fluxState.RecordedEventStore);
        Assert.Contains("TestStateEvent", _fluxState.RecordedEventStore);
    }

    [Fact]
    public async Task FluxState_MutateState_CallsMapper()
    {
        //Act
        await _fluxState.MutateState(_command);

        //Assert
        _mapper.Verify(_ => _.Map(It.IsAny<TestState>(), It.IsAny<TestState>()), Times.Exactly(2));
    }

    [Fact]
    public async Task FluxState_MutateState_WithRefreshMutation_DoesNotRaiseStateChangedEvents()
    {
        //Arrange
        _mutation.Setup(_ => _.StateEvent).Returns("TestRefreshStateEvent");
        _mutation.Setup(_ => _.IsRefreshMutation).Returns(true);
        var stateChangedEventRaised = false;
        var raisedEvent = string.Empty;
        _fluxState.StateChanged += evt =>
        {
            raisedEvent = evt;
            stateChangedEventRaised = true;
            return Task.CompletedTask;
        };

        //Act
        await _fluxState.MutateState(_command);

        //Assert
        Assert.False(stateChangedEventRaised);
        Assert.Contains("TestRefreshStateEvent", _fluxState.RecordedEventStore);
    }

    [Fact]
    public async Task FluxState_MutateState_WithNonRefreshMutation_RaisesStateChangedEvents()
    {
        //Arrange
        var stateChangedEventRaised = false;
        var raisedEvent = string.Empty;
        _fluxState.StateChanged += evt =>
        {
            raisedEvent = evt;
            stateChangedEventRaised = true;
            return Task.CompletedTask;
        };

        //Act
        await _fluxState.MutateState(_command);

        //Assert
        Assert.True(stateChangedEventRaised);
        Assert.Equal("TestStateEvent", raisedEvent);
    }
}
