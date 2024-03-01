//using AutoFixture.AutoMoq;
//using Carlton.Core.Flux.Contracts;
//using Carlton.Core.Flux.Handlers.ViewModels;
//using Carlton.Core.Flux.Models;
//using Carlton.Core.Flux.Tests.Common;
//using Carlton.Core.Flux.Tests.Common.Extensions;
//using Microsoft.Extensions.Logging;
//using MockHttp;

//namespace Carlton.Core.Components.Flux.Test.DecoratorTests.ViewModels;

//public class ViewModelHttpDecoratorTests
//{
//    private readonly IFixture _fixture;
//    private readonly Mock<IViewModelQueryDispatcher<TestState>> _decorated;
//    private readonly Mock<IMutableFluxState<TestState>> _fluxState;
//    private readonly MockHttpHandler mockHttp = new();

//    public ViewModelHttpDecoratorTests()
//    {
//        _fixture = new Fixture().Customize(new AutoMoqCustomization());
//        _decorated = _fixture.Freeze<Mock<IViewModelQueryDispatcher<TestState>>>();
//        _fluxState = _fixture.Freeze<Mock<IMutableFluxState<TestState>>>();
//        _fixture.Freeze<Mock<ILogger<ViewModelHttpDecorator<TestState>>>>();
//        _fixture.Register(() => new HttpClient(mockHttp));
//        _fluxState.Setup(_ => _.State).Returns(new TestState { ClientID = 50, UserID = 107 });
//    }

//    [Theory, AutoData]
//    public async Task Dispatch_DispatchAndHttpRefreshAndMutateStateCalled(TestViewModel vm)
//    {
//        //Arrange
//        var sender = new HttpRefreshCaller();
//        var query = new ViewModelQuery();
//        var sut = _fixture.Create<ViewModelHttpDecorator<TestState>>();
//        _decorated.SetupDispatcher(vm);
//        mockHttp.SetupMockHttpHandler("GET", "http://test.carlton.com/", 200, vm);

//        //Act 
//        await sut.Dispatch<TestViewModel>(sender, query, CancellationToken.None);

//        //Assert
//        _decorated.VerifyDispatch<TestViewModel>(query);
//        _fluxState.VerifyStateMutation(vm, 1);
//        mockHttp.VerifyMockHttpHandler("GET", "http://test.carlton.com/");
//    }

//    [Theory, AutoData]
//    public async Task Dispatch_WithComponentUrlParameters_DispatchAndHttpRefreshAndMutateStateCalled(TestViewModel vm)
//    {
//        //Arrange
//        var sender = new HttpRefreshWithComponentParametersCaller();
//        var query = new ViewModelQuery();
//        var sut = _fixture.Create<ViewModelHttpDecorator<TestState>>();
//        _decorated.SetupDispatcher(vm);
//        mockHttp.SetupMockHttpHandler("GET", "http://test.carlton.com/clients/5/users/10", 200, vm);

//        //Act 
//        await sut.Dispatch<TestViewModel>(sender, query, CancellationToken.None);

//        //Assert
//        _decorated.VerifyDispatch<TestViewModel>(query);
//        _fluxState.VerifyStateMutation(vm, 1);
//        mockHttp.VerifyMockHttpHandler("GET", "http://test.carlton.com/clients/5/users/10");
//    }

//    [Theory, AutoData]
//    public async Task Dispatch_WithStateUrlParameters_DispatchAndHttpRefreshAndMutateStateCalled(TestViewModel vm)
//    {
//        //Arrange
//        var sender = new HttpRefreshWithStateParametersCaller();
//        var query = new ViewModelQuery();
//        var sut = _fixture.Create<ViewModelHttpDecorator<TestState>>();
//        _decorated.SetupDispatcher(vm);
//        mockHttp.SetupMockHttpHandler("GET", $"http://test.carlton.com/clients/50/users/107", 200, vm);

//        //Act 
//        await sut.Dispatch<TestViewModel>(sender, query, CancellationToken.None);

//        //Assert
//        _decorated.VerifyDispatch<TestViewModel>(query);
//        _fluxState.VerifyStateMutation(vm, 1);
//        mockHttp.VerifyMockHttpHandler("GET", "http://test.carlton.com/clients/50/users/107");
//    }

//    [Fact]
//    public async Task Dispatch_NoAttribute_HttpRefreshNotCalled()
//    {
//        //Arrange
//        var sender = new NoRefreshCaller();
//        var query = new ViewModelQuery();
//        var expextedResult = _fixture.Create<TestViewModel>();
//        var sut = _fixture.Create<ViewModelHttpDecorator<TestState>>();
//        _decorated.SetupDispatcher(expextedResult);

//        //Act 
//        var actualResult = await sut.Dispatch<TestViewModel>(sender, query, CancellationToken.None);

//        //Assert
//        Assert.False(mockHttp.InvokedRequests.Any());
//        Assert.Equal(expextedResult, actualResult);
//        _decorated.VerifyDispatch<TestViewModel>(query);
//        _fluxState.VerifyStateMutation(query, 0);
//    }

//    [Fact]
//    public async Task Dispatch_WithNeverAttribute_HttpRefreshNotCalled()
//    {
//        //Arrange
//        var sender = new HttpNeverRefreshCaller();
//        var query = new ViewModelQuery();
//        var expectedResult = _fixture.Create<TestViewModel>();
//        var sut = _fixture.Create<ViewModelHttpDecorator<TestState>>();
//        _decorated.SetupDispatcher(expectedResult);

//        //Act 
//        var actualResult = await sut.Dispatch<TestViewModel>(sender, query, CancellationToken.None);

//        //Assert
//        Assert.False(mockHttp.InvokedRequests.Any());
//        Assert.Equal(expectedResult, actualResult);
//        _decorated.VerifyDispatch<TestViewModel>(query);
//        _fluxState.VerifyStateMutation(query, 0);
//    }

//    [Theory, AutoData]
//    public async Task Dispatch_AssertViewModels(TestViewModel expectedViewModel)
//    {
//        //Arrange
//        mockHttp.SetupMockHttpHandler("GET", "http://test.carlton.com/", 200, expectedViewModel);
//        var sender = new HttpRefreshCaller();
//        var query = new ViewModelQuery();
//        var sut = _fixture.Create<ViewModelHttpDecorator<TestState>>();
//        _decorated.SetupDispatcher(expectedViewModel);

//        //Act 
//        var actualViewModel = await sut.Dispatch<TestViewModel>(sender, query, CancellationToken.None);

//        //Assert
//        Assert.Equal(expectedViewModel, actualViewModel);
//    }

//    [Fact]
//    public async Task Dispatch_WithInvalidHttpUrl_ThrowsViewModelFluxException()
//    {
//        //Arrange
//        var sender = new HttpRefreshWithInvalidHttpUrlCaller();
//        var query = new ViewModelQuery();
//        var sut = _fixture.Create<ViewModelHttpDecorator<TestState>>();

//        //Act 
//        var ex = await Assert.ThrowsAsync<ViewModelFluxException<TestState, TestViewModel>>(async () => await sut.Dispatch<TestViewModel>(sender, query, CancellationToken.None));

//        //Assert
//        Assert.Equal(LogEvents.ViewModel_HTTP_URL_Error, ex.EventID);
//        Assert.Equal(LogEvents.ViewModel_HTTP_URL_ErrorMsg, ex.Message);
//    }

//    [Fact]
//    public async Task Dispatch_WithInvalidComponentUrlParameters_ThrowsViewModelFluxException()
//    {
//        //Arrange
//        var sender = new HttpRefreshWithInvalidParametersCaller();
//        var query = new ViewModelQuery();
//        var sut = _fixture.Create<ViewModelHttpDecorator<TestState>>();

//        //Act 
//        var ex = await Assert.ThrowsAsync<ViewModelFluxException<TestState, TestViewModel>>(async () => await sut.Dispatch<TestViewModel>(sender, query, CancellationToken.None));

//        //Assert
//        Assert.Equal(LogEvents.ViewModel_HTTP_URL_Error, ex.EventID);
//        Assert.Equal(LogEvents.ViewModel_HTTP_URL_ErrorMsg, ex.Message);
//    }

//    [Fact]
//    public async Task Dispatch_WithInvalidJsonParseError_ShouldThrowViewModelFluxException()
//    {
//        //Arrange
//        var sender = new HttpRefreshCaller();
//        var query = new ViewModelQuery();
//        var sut = _fixture.Create<ViewModelHttpDecorator<TestState>>();
//        var unserializableResponse = new { Param1 = 1, Param2 = "Test", WillNotSerialize = typeof(HttpClient) };
//        mockHttp.SetupMockHttpHandler("GET", $"http://test.carlton.com/", 200, unserializableResponse);

//        //Act 
//        var ex = await Assert.ThrowsAsync<ViewModelFluxException<TestState, TestViewModel>>(async () => await sut.Dispatch<TestViewModel>(sender, query, CancellationToken.None));

//        //Assert
//        Assert.Equal(LogEvents.ViewModel_JSON_Error, ex.EventID);
//        Assert.Equal(LogEvents.ViewModel_JSON_ErrorMsg, ex.Message);
//    }

//    [Fact]
//    public async Task Dispatch_WithHttpError_ShouldThrowViewModelFluxException()
//    {
//        //Arrange
//        var sender = new HttpRefreshCaller();
//        var query = new ViewModelQuery();
//        var sut = _fixture.Create<ViewModelHttpDecorator<TestState>>();
//        var vm = new TestViewModel(1, "Test", "Testing");
//        mockHttp.SetupMockHttpHandler("GET", $"http://test.carlton.com/", 500, vm);

//        //Act 
//        var ex = await Assert.ThrowsAsync<ViewModelFluxException<TestState, TestViewModel>>(async () => await sut.Dispatch<TestViewModel>(sender, query, CancellationToken.None));

//        //Assert
//        Assert.Equal(LogEvents.ViewModel_HTTP_Error, ex.EventID);
//        Assert.Equal(LogEvents.ViewModel_HTTP_ErrorMsg, ex.Message);
//    }
//}

