using Carlton.Core.Components.Flux.Contracts;
using Carlton.Core.Components.Flux.Decorators.Mutations;
using Carlton.Core.Components.Flux.Models;
using Carlton.Core.Components.Flux.Test.Common;
using Microsoft.Extensions.Logging;
using MockHttp;
using MockHttp.Json.SystemTextJson;
using Moq;

namespace Carlton.Core.Components.Flux.Test.DecoratorTests.Mutations;

public class MutationHttpDecoratorTests
{
    private readonly Mock<IServiceProvider> _mockServiceProvider = new();
    private readonly Mock<IMutationCommandDispatcher<TestState>> _decorated = new();
    private readonly MockHttpHandler mockHttp = new();
    private readonly Mock<IFluxState<TestState>> _state = new();
    private readonly Mock<ILogger<MutationHttpDecorator<TestState>>> _logger = new();
    private readonly MutationHttpDecorator<TestState> _dispatcher;

    private readonly Action<RequestMatching> HttpHandlerVerifyBaseUrlAction = matching => matching
                                                .Method("POST")
                                                .RequestUri("http://test.carlton.com/");


    private readonly Action<RequestMatching> HttpHandlerVerifyParameterUrlAction = matching => matching
                                                .Method("POST")
                                                .RequestUri("http://test.carlton.com/clients/5/users/10");

    public MutationHttpDecoratorTests()
    {
        mockHttp
            .When(matching => matching
                .Method("POST")
                .RequestUri("http://test.carlton.com/")
            )
            .Respond(with => with
                .StatusCode(200)
                .JsonBody(TestDataGenerator.ExpectedCommand_1)
            );

        mockHttp.When(matching => matching
            .Method("POST")
            .RequestUri("http://test.carlton.com/clients/5/users/10")
            )
            .Respond(with => with
                .StatusCode(200)
                .JsonBody(TestDataGenerator.ExpectedViewModel_1)
            );

        var httpClient = new HttpClient(mockHttp);
        _state.Setup(_ => _.State).Returns(new TestState());
        _dispatcher = new MutationHttpDecorator<TestState>(_decorated.Object, httpClient, _state.Object, _logger.Object);
    }

    [Theory]
    [MemberData(nameof(TestDataGenerator.GetCommandData), MemberType = typeof(TestDataGenerator))]
    public async Task Dispatch_DispatchAndHttpRefreshCalled<TCommand>(TCommand command)
        where TCommand : MutationCommand
    {
        //Act 
        await _dispatcher.Dispatch(command, CancellationToken.None);

        //Assert
        await mockHttp.VerifyAsync(HttpHandlerVerifyBaseUrlAction, IsSent.Exactly(1));
        _decorated.VerifyDispatchCalled(command);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(5)]
    [InlineData(7)]
    public async Task Dispatch_UpdateCommandWithExternalResponseCalled(int serverID)
    {
        //Arrange
        var caller = new HttpRefreshCaller();
        var command = new TestCommand1(caller, -1, "Testing", "Description");

        mockHttp
          .When(matching => matching
              .Method("POST")
              .RequestUri("http://test.carlton.com/")
          )
          .Respond(with => with
              .StatusCode(200)
              .JsonBody(new TestCommand1(caller, serverID, "Testing", "Description"))
          );

        //Act 
        await _dispatcher.Dispatch(command, CancellationToken.None);

        //Assert
        Assert.Equal(serverID, command.SourceSystemID);
    }

    [Theory]
    [MemberData(nameof(TestDataGenerator.GetCommandDataWithComponentParametersCallers), MemberType = typeof(TestDataGenerator))]
    public async Task Dispatch_WithComponentUrlParameters_DispatchAndHttpRefreshCalled<TCommand>(TCommand command)
        where TCommand : MutationCommand
    {
        //Act 
        await _dispatcher.Dispatch(command, CancellationToken.None);

        //Assert
        await mockHttp.VerifyAsync(HttpHandlerVerifyParameterUrlAction, IsSent.Exactly(1));
        _decorated.VerifyDispatchCalled(command);
    }

    [Fact]
    public async Task Dispatch_WithInvalidComponentUrlParameters_ThrowsInvalidOperationException()
    {
        //Arrange
        var expectedMessage = "The HTTP ViewModel refresh endpoint is invalid, following URL parameters were not replaced: {ClientID}, {UserID}";
        var command = new TestCommand1(new HttpRefreshWithInvalidParametersCaller(), -1, "some command", "some description");

        //Act 
        var ex = await Assert.ThrowsAsync<InvalidOperationException>(async () => await _dispatcher.Dispatch(command, CancellationToken.None));

        //Assert
        Assert.Equal(expectedMessage, ex.Message);
    }

    [Theory]
    [MemberData(nameof(TestDataGenerator.GetCommandDataWithStateParametersCallers), MemberType = typeof(TestDataGenerator))]
    public async Task Dispatch_WithStateUrlParameters_DispatchAndHttpRefreshCalled<TCommand>(TCommand command)
        where TCommand : MutationCommand
    {
        //Act 
        await _dispatcher.Dispatch(command, CancellationToken.None);

        //Assert
        await mockHttp.VerifyAsync(HttpHandlerVerifyParameterUrlAction, IsSent.Exactly(1));
        _decorated.VerifyDispatchCalled(command);
    }


    [Fact]
    public async Task Dispatch_NoAttribute_HttpRefreshNotCalled()
    {
        //Arrange
        var command = new TestCommand1(new NoRefreshCaller(), 2, "Testing Again", "xxx");

        //Act 
        await _dispatcher.Dispatch(command, CancellationToken.None);

        //Assert
        Assert.False(mockHttp.InvokedRequests.Any());
        _decorated.VerifyDispatchCalled(command);
    }

    [Fact]
    public async Task Dispatch_WithNeverAttribute_HttpRefreshNotCalled()
    {
        //Arrange
        var command = new TestCommand2(new HttpNeverRefreshCaller(), 2, "Testing Again", 17);

        //Act 
        await _dispatcher.Dispatch(command, CancellationToken.None);

        //Assert
        Assert.False(mockHttp.InvokedRequests.Any());
        _decorated.VerifyDispatchCalled(command);
    }
}

