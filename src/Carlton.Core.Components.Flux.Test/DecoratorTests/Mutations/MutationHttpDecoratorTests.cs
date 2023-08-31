using AutoFixture.AutoMoq;
using Carlton.Core.Components.Flux.Contracts;
using Carlton.Core.Components.Flux.Decorators.Mutations;
using Carlton.Core.Components.Flux.Models;
using Carlton.Core.Components.Flux.Test.Common;
using Carlton.Core.Components.Flux.Test.Common.Extensions;
using Microsoft.Extensions.Logging;
using MockHttp;

namespace Carlton.Core.Components.Flux.Test.DecoratorTests.Mutations;

public class MutationHttpDecoratorTests
{
    private readonly IFixture _fixture;
    private readonly Mock<IMutationCommandDispatcher<TestState>> _decorated;
    private readonly Mock<IMutableFluxState<TestState>> _state;
    private readonly Mock<ILogger<MutationHttpDecorator<TestState>>> _logger;
    private readonly MockHttpHandler mockHttp = new();

    public MutationHttpDecoratorTests()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
        _decorated = _fixture.Freeze<Mock<IMutationCommandDispatcher<TestState>>>();
        _state = _fixture.Freeze<Mock<IMutableFluxState<TestState>>>();
        _logger = _fixture.Freeze<Mock<ILogger<MutationHttpDecorator<TestState>>>>();
        _fixture.Register(() => new HttpClient(mockHttp));
        _state.Setup(_ => _.State).Returns(new TestState());
    }

    [Theory]
    [MemberData(nameof(TestDataGenerator.GetCommandData), MemberType = typeof(TestDataGenerator))]
    public async Task Dispatch_DispatchAndHttpRefreshCalled<TCommand>(TCommand command)
        where TCommand : MutationCommand
    {
        //Arrange
        var sender = new HttpRefreshCaller();
        var sut =  _fixture.Create<MutationHttpDecorator<TestState>>();

        mockHttp.SetupMockHttpHandler("POST", "http://test.carlton.com/", 200, command, command);

        //Act 
        await sut.Dispatch(sender, command, CancellationToken.None);

        //Assert
         mockHttp.VerifyMockHttpHandler("POST", "http://test.carlton.com/");
        _decorated.VerifyDispatchCalled(command);
    }

    [Theory]
    [MemberData(nameof(TestDataGenerator.GetCommandData), MemberType = typeof(TestDataGenerator))]
    public async Task Dispatch_DispatchAndHttpRefreshWithComponentParametersCalled<TCommand>(TCommand command)
      where TCommand : MutationCommand
    {
        //Arrange
        var sender = new HttpRefreshWithComponentParametersCaller();
        mockHttp.SetupMockHttpHandler("POST", "http://test.carlton.com/clients/5/users/10", 200, command, command);
        var sut = _fixture.Create<MutationHttpDecorator<TestState>>();

        //Act 
        await sut.Dispatch(sender, command, CancellationToken.None);

        //Assert
        mockHttp.VerifyMockHttpHandler("POST", "http://test.carlton.com/clients/5/users/10");
        _decorated.VerifyDispatchCalled(command);
    }

    [Theory]
    [MemberData(nameof(TestDataGenerator.GetCommandData), MemberType = typeof(TestDataGenerator))]
    public async Task Dispatch_DispatchAndHttpRefreshWithStateParametersCalled<TCommand>(TCommand command)
      where TCommand : MutationCommand
    {
        //Arrange
        var sender = new HttpRefreshWithStateParametersCaller();
        mockHttp.SetupMockHttpHandler("POST", "http://test.carlton.com/clients/5/users/10", 200, command, command);
        var sut = _fixture.Create<MutationHttpDecorator<TestState>>();

        //Act 
        await sut.Dispatch(sender, command, CancellationToken.None);

        //Assert
        mockHttp.VerifyMockHttpHandler("POST", "http://test.carlton.com/clients/5/users/10");
        _decorated.VerifyDispatchCalled(command);
    }

    [Theory, AutoData]
    public async Task Dispatch_UpdateCommandWithExternalResponseCalled(int sourceSystemID)
    {
        //Arrange
        var caller = new HttpRefreshCaller();
        var command = _fixture.Create<TestCommand1>();
        var responseCommand = _fixture.Create<TestCommand1>();
        var sut = _fixture.Create<MutationHttpDecorator<TestState>>();

        mockHttp.SetupMockHttpHandler("POST", "http://test.carlton.com/", 200, command, responseCommand);

        //Act 
        await sut.Dispatch(caller, command, CancellationToken.None);

        //Assert
        Assert.Equal(sourceSystemID, command.SourceSystemID);
    }

    [Fact]
    public async Task Dispatch_WithInvalidComponentUrlParameters_ThrowsInvalidOperationException()
    {
        //Arrange
        var expectedMessage = "The HTTP ViewModel refresh endpoint is invalid, following URL parameters were not replaced: {ClientID}, {UserID}";
        var caller = new HttpRefreshWithInvalidParametersCaller();
        var command = _fixture.Create<TestCommand1>();
        var sut = _fixture.Create<MutationHttpDecorator<TestState>>();

        //Act 
        var ex = await Assert.ThrowsAsync<ArgumentException>(async () => await cut.Dispatch(caller, command, CancellationToken.None));

        //Assert
        Assert.Equal(expectedMessage, ex.Message);
    }

    [Fact]
    public async Task Dispatch_NoAttribute_HttpRefreshNotCalled()
    {
        //Arrange
        var caller = new NoRefreshCaller();
        var command = _fixture.Create<TestCommand1>();
        var sut = _fixture.Create<MutationHttpDecorator<TestState>>();

        //Act 
        await cut.Dispatch(caller, command, CancellationToken.None);

        //Assert
        Assert.False(mockHttp.InvokedRequests.Any());
        _decorated.VerifyDispatchCalled(command);
    }

    [Fact]
    public async Task Dispatch_WithNeverAttribute_HttpRefreshNotCalled()
    {
        //Arrange
        var caller = new HttpNeverRefreshCaller();
        var command = _fixture.Create<TestCommand1>();
        var sut = _fixture.Create<MutationHttpDecorator<TestState>>();

        //Act 
        await sut.Dispatch(caller, command, CancellationToken.None);

        //Assert
        Assert.False(mockHttp.InvokedRequests.Any());
        _decorated.VerifyDispatchCalled(command);
    }
}

