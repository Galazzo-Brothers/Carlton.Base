//using AutoFixture.AutoMoq;
//using Carlton.Core.Flux.Contracts;
//using Carlton.Core.Flux.State;
//using Carlton.Core.Flux.Tests.Common;
//using Carlton.Core.Flux.Tests.Common.Extensions;

//namespace Carlton.Core.Flux.Tests.StateTests;

//public class StateTests
//{
//    private readonly IFixture _fixture;
//    private readonly TestState _state;
//    private readonly Mock<IFluxStateMutation<TestState, TestCommand1>> _mutation;
//    private readonly Mock<IMapper> _mapper;

//    public StateTests()
//    {
//        //Setup the fixture with Moq
//        _fixture = new Fixture().Customize(new AutoMoqCustomization());

//        //Setup mocked dependencies
//        _state = _fixture.Freeze<TestState>();
//        _mutation = _fixture.Freeze<Mock<IFluxStateMutation<TestState, TestCommand1>>>();
//        _mapper = _fixture.Freeze<Mock<IMapper>>();

//        //configure the service provider 
//        _fixture.Freeze<Mock<IMutationResolver<TestState>>>().SetUpMutationResolver(_mutation.Object);
//    }

//    [Fact]
//    public async Task FluxState_MutateState_CallsMutation()
//    {
//        //Arrange
//        var command = _fixture.Create<TestCommand1>();
//        var sut = _fixture.Create<FluxState<TestState>>();

//        //Act
//        await sut.MutateState(command);

//        //Assert
//        _mutation.VerifyMutation(_state, command);
//    }

//    [Theory, AutoData]
//    public async Task FluxState_MutateState_RecordsStateEvents(string stateEvent)
//    {
//        //Arrange
//        _mutation.SetUpMutation(stateEvent, false);
//        var command = _fixture.Create<TestCommand1>();
//        var sut = _fixture.Create<FluxState<TestState>>();

//        //Act
//        await sut.MutateState(command);

//        //Assert
//        Assert.NotEmpty(sut.RecordedEventStore);
//        Assert.Contains(stateEvent, sut.RecordedEventStore);
//    }

//    [Fact]
//    public async Task FluxState_MutateState_CallsMapper()
//    {
//        //Arrange
//        var command = _fixture.Create<TestCommand1>();
//        var sut = _fixture.Create<FluxState<TestState>>();

//        //Act
//        await sut.MutateState(command);

//        //Assert
//        _mapper.VerifyMapper(2);
//    }

//    [Theory, AutoData]
//    public async Task FluxState_MutateState_WithRefreshMutation_DoesNotRaiseStateChangedEvents(string stateEvent)
//    {
//        //Arrange
//        var stateChangedEventRaised = false;
//        var raisedEvent = string.Empty;
//        var command = _fixture.Create<TestCommand1>();
//        var sut = _fixture.Create<FluxState<TestState>>();
//        _mutation.SetUpMutation(stateEvent, true);
//        sut.StateChanged += evt =>
//        {
//            raisedEvent = evt;
//            stateChangedEventRaised = true;
//            return Task.CompletedTask;
//        };

//        //Act
//        await sut.MutateState(command);

//        //Assert
//        Assert.False(stateChangedEventRaised);
//        Assert.Contains(stateEvent, sut.RecordedEventStore);
//    }

//    [Theory, AutoData]
//    public async Task FluxState_MutateState_WithNonRefreshMutation_RaisesStateChangedEvents(string stateEvent)
//    {
//        //Arrange
//        var stateChangedEventRaised = false;
//        var raisedEvent = string.Empty;
//        var command = _fixture.Create<TestCommand1>();
//        var sut = _fixture.Create<FluxState<TestState>>();
//        _mutation.SetUpMutation(stateEvent, false);
//        sut.StateChanged += evt =>
//        {
//            raisedEvent = evt;
//            stateChangedEventRaised = true;
//            return Task.CompletedTask;
//        };

//        //Act
//        await sut.MutateState(command);

//        //Assert
//        Assert.True(stateChangedEventRaised);
//        Assert.Equal(stateEvent, raisedEvent);
//    }
//}
