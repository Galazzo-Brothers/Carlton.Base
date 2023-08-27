using Carlton.Core.Components.Flux.Contracts;
using Carlton.Core.Components.Flux.Decorators.ViewModels;
using Carlton.Core.Components.Flux.Models;
using Carlton.Core.Components.Flux.Test.Common;
using Microsoft.Extensions.Logging;
using MockHttp;
using MockHttp.Json.SystemTextJson;
using Moq;

namespace Carlton.Core.Components.Flux.Test.DecoratorTests.ViewModels;

public class ViewModelHttpDecoratorTests
{
    private readonly Mock<IViewModelQueryDispatcher<TestState>> _decorated = new();
    private readonly MockHttpHandler mockHttp = new();
    private readonly Mock<IMutableFluxState<TestState>> _state = new();
    private readonly Mock<ILogger<ViewModelHttpDecorator<TestState>>> _logger = new();
    private readonly ViewModelHttpDecorator<TestState> _dispatcher;

    private readonly Action<RequestMatching> HttpHandlerVerifyBaseUrlAction = matching => matching
                                                .Method("GET")
                                                .RequestUri("http://test.carlton.com/");


    private readonly Action<RequestMatching> HttpHandlerVerifyParameterUrlAction = matching => matching
                                                .Method("GET")
                                                .RequestUri("http://test.carlton.com/clients/5/users/10");

    public ViewModelHttpDecoratorTests()
    {
        SetupMockHttpEndpoint(TestDataGenerator.ExpectedViewModel_1);

        SetupMockHttpParameterizedEndpoint(TestDataGenerator.ExpectedViewModel_1);

        var httpClient = new HttpClient(mockHttp);
        _state.Setup(_ => _.State).Returns(new TestState());
        _dispatcher = new ViewModelHttpDecorator<TestState>(_decorated.Object, httpClient, _state.Object, _logger.Object);
    }

    [Theory]
    [MemberData(nameof(TestDataGenerator.GetViewModelData), MemberType = typeof(TestDataGenerator))]
    public async Task Dispatch_DispatchAndHttpRefreshAndMutateStateCalled<TViewModel>(TViewModel vm)
    {
        //Arrange
        SetupMockHttpEndpoint(vm);
        var query = new ViewModelQuery(new HttpRefreshCaller());

        //Act 
        await _dispatcher.Dispatch<TViewModel>(query, CancellationToken.None);

        //Assert
        VerifyHttpCall();
        VerifyDispatch<TViewModel>(query);
        VerifyStateMutation(vm);
    }

    [Theory]
    [MemberData(nameof(TestDataGenerator.GetViewModelData), MemberType = typeof(TestDataGenerator))]
    public async Task Dispatch_WithComponentUrlParameters_DispatchAndHttpRefreshAndMutateStateCalled<TViewModel>(TViewModel vm)
    {
        //Arrange
        SetupMockHttpParameterizedEndpoint(vm);
        var query = new ViewModelQuery(new HttpRefreshWithComponentParametersCaller());

        //Act 
        await _dispatcher.Dispatch<TViewModel>(query, CancellationToken.None);

        //Assert
        VerifyParameterizedHttpCall();
        VerifyDispatch<TViewModel>(query);
        VerifyStateMutation(vm);
    }

    [Theory]
    [MemberData(nameof(TestDataGenerator.GetViewModelData), MemberType = typeof(TestDataGenerator))]
    public async Task Dispatch_WithStateUrlParameters_DispatchAndHttpRefreshAndMutateStateCalled<TViewModel>(TViewModel vm)
    {
        //Arrange
        SetupMockHttpParameterizedEndpoint(vm);
        var query = new ViewModelQuery(new HttpRefreshWithStateParametersCaller());

        //Act 
        await _dispatcher.Dispatch<TViewModel>(query, CancellationToken.None);

        //Assert
        VerifyParameterizedHttpCall();
        VerifyDispatch<TViewModel>(query);
        VerifyStateMutation(vm);
    }

    [Fact]
    public async Task Dispatch_WithInvalidComponentUrlParameters_ThrowsInvalidOperationException()
    {
        //Arrange
        var expectedMessage = "The HTTP ViewModel refresh endpoint is invalid, following URL parameters were not replaced: {ClientID}, {UserID}";
        var query = new ViewModelQuery(new HttpRefreshWithInvalidParametersCaller());

        //Act 
        var ex = await Assert.ThrowsAsync<InvalidOperationException>(async () => await _dispatcher.Dispatch<TestViewModel1>(query, CancellationToken.None));

        //Assert
        Assert.Equal(expectedMessage, ex.Message);
    }

    [Fact]
    public async Task Dispatch_NoAttribute_HttpRefreshNotCalled()
    {
        //Arrange
        var query = new ViewModelQuery(new NoRefreshCaller());

        //Act 
        await _dispatcher.Dispatch<TestViewModel1>(query, CancellationToken.None);

        //Assert
        Assert.False(mockHttp.InvokedRequests.Any());
        VerifyDispatch<TestViewModel1>(query);
        VerifyStateMutationNotCalled<TestViewModel1>();
    }

    [Fact]
    public async Task Dispatch_WithNeverAttribute_HttpRefreshNotCalled()
    {
        //Arrange
        var query = new ViewModelQuery(new HttpNeverRefreshCaller());

        //Act 
        await _dispatcher.Dispatch<TestViewModel1>(query, CancellationToken.None);

        //Assert
        Assert.False(mockHttp.InvokedRequests.Any());
        _decorated.VerifyDispatchCalled<TestViewModel1>(query);
        _state.Verify(_ => _.MutateState(It.IsAny<TestViewModel1>()), Times.Never());
    }

    [Theory]
    [MemberData(nameof(TestDataGenerator.GetViewModelData), MemberType = typeof(TestDataGenerator))]
    public async Task Dispatch_AssertViewModels<TViewModel>(TViewModel expectedViewModel)
    {
        //Arrange
        mockHttp
        .When(matching => matching
            .Method("GET")
            .RequestUri("http://test.carlton.com/")
        )
        .Respond(with => with
            .StatusCode(200)
            .JsonBody(expectedViewModel)
        );
        _decorated.Setup(_ => _.Dispatch<TViewModel>(It.IsAny<ViewModelQuery>(), CancellationToken.None)).Returns(Task.FromResult(expectedViewModel));
        var query = new ViewModelQuery(new HttpRefreshCaller());

        //Act 
        var actualViewModel = await _dispatcher.Dispatch<TViewModel>(query, CancellationToken.None);

        //Assert
        Assert.Equal(expectedViewModel, actualViewModel);
    }

    private void SetupMockHttpEndpoint<T>(T response)
    {
        mockHttp
            .When(matching => matching
                .Method("GET")
                .RequestUri("http://test.carlton.com/")
            )
            .Respond(with => with
                .StatusCode(200)
                .JsonBody(response)
            );
    }

    private void SetupMockHttpParameterizedEndpoint<T>(T response)
    {
        mockHttp.When(matching => matching
            .Method("GET")
            .RequestUri("http://test.carlton.com/clients/5/users/10")
            )
            .Respond(with => with
                .StatusCode(200)
                .JsonBody(response)
            );
    }

    private void VerifyStateMutation<TViewModel>(TViewModel vm)
    {
        _state.Verify(_ => _.MutateState(vm));
    }

    private void VerifyStateMutationNotCalled<TViewModel>()
    {
        _state.Verify(_ => _.MutateState<TViewModel>(It.IsAny<TViewModel>()), Times.Never());
    }

    private void VerifyDispatch<TViewModel>(ViewModelQuery query)
    {
        _decorated.VerifyDispatchCalled<TViewModel>(query);
    }

    private void VerifyHttpCall()
    {
        mockHttp.Verify(HttpHandlerVerifyBaseUrlAction, IsSent.Exactly(1));
    }

    private void VerifyParameterizedHttpCall()
    {
        mockHttp.Verify(HttpHandlerVerifyParameterUrlAction, IsSent.Exactly(1));
    }
}

