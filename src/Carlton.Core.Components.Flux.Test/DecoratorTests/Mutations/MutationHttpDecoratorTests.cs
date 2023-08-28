using Carlton.Core.Components.Flux.Contracts;
using Carlton.Core.Components.Flux.Decorators.Mutations;
using Carlton.Core.Components.Flux.Models;
using Carlton.Core.Components.Flux.Test.Common;
using Microsoft.Extensions.Logging;
using MockHttp;
using Moq;

namespace Carlton.Core.Components.Flux.Test.DecoratorTests.Mutations;

public class MutationHttpDecoratorTests
{
    private readonly Mock<IMutationCommandDispatcher<TestState>> _decorated = new();
    private readonly MockHttpHandler mockHttp = new();
    private readonly Mock<IMutableFluxState<TestState>> _state = new();
    private readonly Mock<ILogger<MutationHttpDecorator<TestState>>> _logger = new();
    private readonly MutationHttpDecorator<TestState> _dispatcher;

    public MutationHttpDecoratorTests()
    {
        var httpClient = new HttpClient(mockHttp);
        _state.Setup(_ => _.State).Returns(new TestState());
        _dispatcher = new MutationHttpDecorator<TestState>(_decorated.Object, httpClient, _state.Object, _logger.Object);
    }

    [Theory]
    [MemberData(nameof(TestDataGenerator.GetCommandData), MemberType = typeof(TestDataGenerator))]
    public async Task Dispatch_DispatchAndHttpRefreshCalled<TCommand>(TCommand command)
        where TCommand : MutationCommand
    {
        //Arrange
        var sender = new HttpRefreshCaller();
        mockHttp.SetupMockHttpHandler("POST", "http://test.carlton.com/", 200, command, command);

        //Act 
        await _dispatcher.Dispatch(sender, command, CancellationToken.None);

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

        //Act 
        await _dispatcher.Dispatch(sender, command, CancellationToken.None);

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

        //Act 
        await _dispatcher.Dispatch(sender, command, CancellationToken.None);

        //Assert
        mockHttp.VerifyMockHttpHandler("POST", "http://test.carlton.com/clients/5/users/10");
        _decorated.VerifyDispatchCalled(command);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(5)]
    [InlineData(7)]
    public async Task Dispatch_UpdateCommandWithExternalResponseCalled(int sourceSystemID)
    {
        //Arrange
        var caller = new HttpRefreshCaller();
        var command = new TestCommand1(-1, "Testing", "Description");
        var responseCommand = new TestCommand1(sourceSystemID, "Testing", "Description");
        mockHttp.SetupMockHttpHandler("POST", "http://test.carlton.com/", 200, command, responseCommand);

        //Act 
        await _dispatcher.Dispatch(caller, command, CancellationToken.None);

        //Assert
        Assert.Equal(sourceSystemID, command.SourceSystemID);
    }

    [Fact]
    public async Task Dispatch_WithInvalidComponentUrlParameters_ThrowsInvalidOperationException()
    {
        //Arrange
        var expectedMessage = "The HTTP ViewModel refresh endpoint is invalid, following URL parameters were not replaced: {ClientID}, {UserID}";
        var caller = new HttpRefreshWithInvalidParametersCaller();
        var command = new TestCommand1(-1, "some command", "some description");

        //Act 
        var ex = await Assert.ThrowsAsync<InvalidOperationException>(async () => await _dispatcher.Dispatch(caller, command, CancellationToken.None));

        //Assert
        Assert.Equal(expectedMessage, ex.Message);
    }

    [Fact]
    public async Task Dispatch_NoAttribute_HttpRefreshNotCalled()
    {
        //Arrange
        var caller = new NoRefreshCaller();
        var command = new TestCommand1(2, "Testing Again", "This is a test");

        //Act 
        await _dispatcher.Dispatch(caller, command, CancellationToken.None);

        //Assert
        Assert.False(mockHttp.InvokedRequests.Any());
        _decorated.VerifyDispatchCalled(command);
    }

    [Fact]
    public async Task Dispatch_WithNeverAttribute_HttpRefreshNotCalled()
    {
        //Arrange
        var caller = new HttpNeverRefreshCaller();
        var command = new TestCommand2(2, "Testing Again", 17);

        //Act 
        await _dispatcher.Dispatch(caller,command, CancellationToken.None);

        //Assert
        Assert.False(mockHttp.InvokedRequests.Any());
        _decorated.VerifyDispatchCalled(command);
    }
}

