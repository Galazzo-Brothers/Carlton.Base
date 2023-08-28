using Carlton.Core.Components.Flux.Contracts;
using Carlton.Core.Components.Flux.Decorators.ViewModels;
using Carlton.Core.Components.Flux.Models;
using Carlton.Core.Components.Flux.Test.Common;
using Microsoft.Extensions.Logging;
using MockHttp;
using Moq;

namespace Carlton.Core.Components.Flux.Test.DecoratorTests.ViewModels;

public class ViewModelHttpDecoratorTests
{
    private readonly Mock<IViewModelQueryDispatcher<TestState>> _decorated = new();
    private readonly MockHttpHandler mockHttp = new();
    private readonly Mock<IMutableFluxState<TestState>> _state = new();
    private readonly Mock<ILogger<ViewModelHttpDecorator<TestState>>> _logger = new();
    private readonly ViewModelHttpDecorator<TestState> _dispatcher;

    public ViewModelHttpDecoratorTests()
    {
        var httpClient = new HttpClient(mockHttp);
        _state.Setup(_ => _.State).Returns(new TestState());
        _dispatcher = new ViewModelHttpDecorator<TestState>(_decorated.Object, httpClient, _state.Object, _logger.Object);
    }

    [Theory]
    [MemberData(nameof(TestDataGenerator.GetViewModelData), MemberType = typeof(TestDataGenerator))]
    public async Task Dispatch_DispatchAndHttpRefreshAndMutateStateCalled<TViewModel>(TViewModel vm)
    {
        //Arrange
        mockHttp.SetupMockHttpHandler("GET", "http://test.carlton.com/", 200, vm);
        var sender = new HttpRefreshCaller();
        var query = new ViewModelQuery();

        //Act 
        await _dispatcher.Dispatch<TViewModel>(sender, query, CancellationToken.None);

        //Assert
        mockHttp.VerifyMockHttpHandler("GET", "http://test.carlton.com/");
        _decorated.VerifyDispatch<TViewModel>(query);
        _state.VerifyStateMutation(vm, 1);
    }

    [Theory]
    [MemberData(nameof(TestDataGenerator.GetViewModelData), MemberType = typeof(TestDataGenerator))]
    public async Task Dispatch_WithComponentUrlParameters_DispatchAndHttpRefreshAndMutateStateCalled<TViewModel>(TViewModel vm)
    {
        //Arrange
        mockHttp.SetupMockHttpHandler("GET", "http://test.carlton.com/clients/5/users/10", 200, vm);
        var sender = new HttpRefreshWithComponentParametersCaller();
        var query = new ViewModelQuery();

        //Act 
        await _dispatcher.Dispatch<TViewModel>(sender, query, CancellationToken.None);

        //Assert
        mockHttp.VerifyMockHttpHandler("GET", "http://test.carlton.com/clients/5/users/10");
        _decorated.VerifyDispatch<TViewModel>(query);
        _state.VerifyStateMutation(vm, 1);
    }

    [Theory]
    [MemberData(nameof(TestDataGenerator.GetViewModelData), MemberType = typeof(TestDataGenerator))]
    public async Task Dispatch_WithStateUrlParameters_DispatchAndHttpRefreshAndMutateStateCalled<TViewModel>(TViewModel vm)
    {
        //Arrange
        mockHttp.SetupMockHttpHandler("GET", "http://test.carlton.com/clients/5/users/10", 200, vm);
        var sender = new HttpRefreshWithStateParametersCaller();
        var query = new ViewModelQuery();

        //Act 
        await _dispatcher.Dispatch<TViewModel>(sender, query, CancellationToken.None);

        //Assert
        mockHttp.VerifyMockHttpHandler("GET", "http://test.carlton.com/clients/5/users/10");
        _decorated.VerifyDispatch<TViewModel>(query);
        _state.VerifyStateMutation(vm, 1);
    }

    [Fact]
    public async Task Dispatch_WithInvalidComponentUrlParameters_ThrowsInvalidOperationException()
    {
        //Arrange
        var expectedMessage = "The HTTP ViewModel refresh endpoint is invalid, following URL parameters were not replaced: {ClientID}, {UserID}";
        var sender = new HttpRefreshWithInvalidParametersCaller();
        var query = new ViewModelQuery();

        //Act 
        var ex = await Assert.ThrowsAsync<InvalidOperationException>(async () => await _dispatcher.Dispatch<TestViewModel1>(sender, query, CancellationToken.None));

        //Assert
        Assert.Equal(expectedMessage, ex.Message);
    }

    [Fact]
    public async Task Dispatch_NoAttribute_HttpRefreshNotCalled()
    {
        //Arrange
        var sender = new NoRefreshCaller();
        var query = new ViewModelQuery();

        //Act 
        await _dispatcher.Dispatch<TestViewModel1>(sender,query, CancellationToken.None);

        //Assert
        Assert.False(mockHttp.InvokedRequests.Any());
        _decorated.VerifyDispatch<TestViewModel1>(query);
        _state.VerifyStateMutation(query, 0);
    }

    [Fact]
    public async Task Dispatch_WithNeverAttribute_HttpRefreshNotCalled()
    {
        //Arrange
        var sender = new HttpNeverRefreshCaller();
        var query = new ViewModelQuery();

        //Act 
        await _dispatcher.Dispatch<TestViewModel1>(sender, query, CancellationToken.None);

        //Assert
        Assert.False(mockHttp.InvokedRequests.Any());
        _decorated.VerifyDispatch<TestViewModel1>(query);
        _state.VerifyStateMutation(query, 0);
    }

    [Theory]
    [MemberData(nameof(TestDataGenerator.GetViewModelData), MemberType = typeof(TestDataGenerator))]
    public async Task Dispatch_AssertViewModels<TViewModel>(TViewModel expectedViewModel)
    {
        //Arrange
        mockHttp.SetupMockHttpHandler("GET", "http://test.carlton.com/", 200, expectedViewModel);
        _decorated.SetupDispatcher(expectedViewModel);
        var sender = new HttpRefreshCaller();
        var query = new ViewModelQuery();

        //Act 
        var actualViewModel = await _dispatcher.Dispatch<TViewModel>(sender, query, CancellationToken.None);

        //Assert
        Assert.Equal(expectedViewModel, actualViewModel);
    }
}

