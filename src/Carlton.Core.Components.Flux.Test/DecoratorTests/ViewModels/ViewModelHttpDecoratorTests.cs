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
    private readonly Mock<IServiceProvider> _mockServiceProvider = new();
    private readonly Mock<IViewModelQueryDispatcher<TestState>> _decorated = new();
    private readonly MockHttpHandler mockHttp = new();
    private readonly Mock<IFluxState<TestState>> _state = new();
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
        mockHttp
            .When(matching => matching
                .Method("GET")
                .RequestUri("http://test.carlton.com/")
            )
            .Respond(with => with
                .StatusCode(200)
                .JsonBody(TestDataGenerator.ExpectedViewModel_1)
            );

        mockHttp.When(matching => matching
            .Method("GET")
            .RequestUri("http://test.carlton.com/clients/5/users/10")
            )
            .Respond(with => with
                .StatusCode(200)
                .JsonBody(TestDataGenerator.ExpectedViewModel_1)
            );

        var httpClient = new HttpClient(mockHttp);

        _dispatcher = new ViewModelHttpDecorator<TestState>(_decorated.Object, httpClient, _state.Object, _logger.Object);
    }

    [Theory]
    [MemberData(nameof(TestDataGenerator.GetViewModelData), MemberType = typeof(TestDataGenerator))]
    public async Task Dispatch_DispatchAndHttpRefreshCalled<TViewModel>(TViewModel vm)
    {
        //Arrange
        var query = new ViewModelQuery(new HttpRefreshCaller());

        //Act 
        await _dispatcher.Dispatch<TViewModel>(query, CancellationToken.None);

        //Assert
        await mockHttp.VerifyAsync(HttpHandlerVerifyBaseUrlAction, IsSent.Exactly(1));
        _decorated.VerifyDispatchCalled<TViewModel>(query);
    }

    [Theory]
    [MemberData(nameof(TestDataGenerator.GetViewModelData), MemberType = typeof(TestDataGenerator))]
    public async Task Dispatch_WithComponentUrlParameters_DispatchAndHttpRefreshCalled<TViewModel>(TViewModel vm)
    {
        //Arrange
        var query = new ViewModelQuery(new HttpRefreshWithParametersCaller());

        //Act 
        await _dispatcher.Dispatch<TViewModel>(query, CancellationToken.None);

        //Assert
        await mockHttp.VerifyAsync(HttpHandlerVerifyParameterUrlAction, IsSent.Exactly(1));
        _decorated.VerifyDispatchCalled<TViewModel>(query);
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

    [Theory]
    [MemberData(nameof(TestDataGenerator.GetViewModelData), MemberType = typeof(TestDataGenerator))]
    public async Task Dispatch_WithStateUrlParameters_DispatchAndHttpRefreshCalled<TViewModel>(TViewModel vm)
    {
        //Arrange
        var query = new ViewModelQuery(new HttpRefreshWithParametersCaller());

        //Act 
        await _dispatcher.Dispatch<TViewModel>(query, CancellationToken.None);

        //Assert
        await mockHttp.VerifyAsync(HttpHandlerVerifyParameterUrlAction, IsSent.Exactly(1));
        _decorated.VerifyDispatchCalled<TViewModel>(query);
    }

    [Fact]
    public async Task Dispatch_NoAttribute_HttpRefreshNotCalled()
    {
        //Arrange
        var query = new ViewModelQuery(new NoRefreshCaller());

        //Act 
        await _dispatcher.Dispatch<TestViewModel1>(query, CancellationToken.None);

        //Assert
        await mockHttp.VerifyAsync(matching => matching
                                                  .Method("GET")
                                                  .RequestUri("http://test.carlton.com/clients/5/users/10"), IsSent.Exactly(0));
        _decorated.VerifyDispatchCalled<TestViewModel1>(query);
    }

    [Fact]
    public async Task Dispatch_WithNeverAttribute_HttpRefreshNotCalled()
    {
        //Arrange
        var query = new ViewModelQuery(new HttpNeverRefreshCaller());

        //Act 
        await _dispatcher.Dispatch<TestViewModel1>(query, CancellationToken.None);

        //Assert
        await mockHttp.VerifyAsync(matching => matching
                                                  .Method("GET")
                                                  .RequestUri("http://test.carlton.com/clients/5/users/10"), IsSent.Exactly(0));
        _decorated.VerifyDispatchCalled<TestViewModel1>(query);
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
}

