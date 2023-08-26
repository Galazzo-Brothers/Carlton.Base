using Carlton.Core.Components.Flux.Contracts;
using Carlton.Core.Components.Flux.State;
using Carlton.Core.Components.Flux.Test.Common;
using Microsoft.Extensions.Logging;
using Moq;

namespace Carlton.Core.Components.Flux.Test.StateTests;

public class StateTests
{
    private readonly TestState _state = new();
    private readonly Mock<IServiceProvider> _serviceProvider = new();
    private readonly MutationResolver<TestState> _mutationResolver;
    private readonly Mock<ILogger<FluxState<TestState>>> _logger = new();
    private readonly FluxState<TestState> _fluxState;
    private readonly Mock<IFluxStateMutation<TestState, TestCommand1>> _mutation = new();
    private readonly TestCommand1 _command = new TestCommand1(new object(), 2, "test command", "this is a test");

    public StateTests()
    {
        _mutationResolver = new MutationResolver<TestState>(_serviceProvider.Object);
        _fluxState = new FluxState<TestState>(_state, _mutationResolver, _logger.Object);
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
    public async Task FluxState_MutateState_RaisesStateChangedEvents()
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

    [Theory]
    [InlineData(100, 200)]
    [InlineData(20, 50)]
    public async Task FluxState_MutateState_UpdatesState(int clientID, int userID)
    {
        //Arrange
        _mutation.Setup(_ => _.Mutate(_state, _command)).Returns(new TestState { ClientID = clientID, UserID = userID });


        //Act
        await _fluxState.MutateState(_command);

        //Assert
        Assert.Equal(clientID, _state.ClientID);
        Assert.Equal(userID, _state.UserID);
    }
}
