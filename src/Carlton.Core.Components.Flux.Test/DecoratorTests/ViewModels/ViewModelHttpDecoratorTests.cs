using AutoFixture.AutoMoq;
using Carlton.Core.Components.Flux.Contracts;
using Carlton.Core.Components.Flux.Decorators.ViewModels;
using Carlton.Core.Components.Flux.Models;
using Carlton.Core.Components.Flux.Test.Common;
using Carlton.Core.Components.Flux.Test.Common.Extensions;
using Microsoft.Extensions.Logging;
using MockHttp;

namespace Carlton.Core.Components.Flux.Test.DecoratorTests.ViewModels;

public class ViewModelHttpDecoratorTests
{
    private readonly IFixture _fixture;
    private readonly Mock<IViewModelQueryDispatcher<TestState>> _decorated;
    private readonly Mock<IMutableFluxState<TestState>> _fluxState;
    private readonly Mock<ILogger<ViewModelHttpDecorator<TestState>>> _logger;
    private readonly MockHttpHandler mockHttp = new();

    public ViewModelHttpDecoratorTests()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
        _decorated = _fixture.Freeze<Mock<IViewModelQueryDispatcher<TestState>>>();
        _fluxState = _fixture.Freeze<Mock<IMutableFluxState<TestState>>>();
        _logger = _fixture.Freeze<Mock<ILogger<ViewModelHttpDecorator<TestState>>>>();
        _fixture.Register(() => new HttpClient(mockHttp));
        _fluxState.Setup(_ => _.State).Returns(new TestState { ClientID = 50, UserID = 107 });
    }

    [Theory]
    [MemberData(nameof(TestDataGenerator.GetViewModelData), MemberType = typeof(TestDataGenerator))]
    public async Task Dispatch_DispatchAndHttpRefreshAndMutateStateCalled<TViewModel>(TViewModel vm)
    {
        //Arrange
        var sender = new HttpRefreshCaller();
        var query = new ViewModelQuery();
        var sut = _fixture.Create<ViewModelHttpDecorator<TestState>>();
        _decorated.SetupDispatcher(vm);
        mockHttp.SetupMockHttpHandler("GET", "http://test.carlton.com/", 200, vm);

        //Act 
        await sut.Dispatch<TViewModel>(sender, query, CancellationToken.None);

        //Assert
        _decorated.VerifyDispatch<TViewModel>(query);
        _fluxState.VerifyStateMutation(vm, 1);
        mockHttp.VerifyMockHttpHandler("GET", "http://test.carlton.com/");
    }

    [Theory]
    [MemberData(nameof(TestDataGenerator.GetViewModelData), MemberType = typeof(TestDataGenerator))]
    public async Task Dispatch_WithComponentUrlParameters_DispatchAndHttpRefreshAndMutateStateCalled<TViewModel>(TViewModel vm)
    {
        //Arrange
        var sender = new HttpRefreshWithComponentParametersCaller();
        var query = new ViewModelQuery();
        var sut = _fixture.Create<ViewModelHttpDecorator<TestState>>();
        _decorated.SetupDispatcher(vm);
        mockHttp.SetupMockHttpHandler("GET", "http://test.carlton.com/clients/5/users/10", 200, vm);

        //Act 
        await sut.Dispatch<TViewModel>(sender, query, CancellationToken.None);

        //Assert
        _decorated.VerifyDispatch<TViewModel>(query);
        _fluxState.VerifyStateMutation(vm, 1);
        mockHttp.VerifyMockHttpHandler("GET", "http://test.carlton.com/clients/5/users/10");
    }

    [Theory]
    [MemberData(nameof(TestDataGenerator.GetViewModelData), MemberType = typeof(TestDataGenerator))]
    public async Task Dispatch_WithStateUrlParameters_DispatchAndHttpRefreshAndMutateStateCalled<TViewModel>(TViewModel vm)
    {
        //Arrange
        var sender = new HttpRefreshWithStateParametersCaller();
        var query = new ViewModelQuery();
        var sut = _fixture.Create<ViewModelHttpDecorator<TestState>>();
        _decorated.SetupDispatcher(vm);
        mockHttp.SetupMockHttpHandler("GET", $"http://test.carlton.com/clients/50/users/107", 200, vm);

        //Act 
        await sut.Dispatch<TViewModel>(sender, query, CancellationToken.None);

        //Assert
        _decorated.VerifyDispatch<TViewModel>(query);
        _fluxState.VerifyStateMutation(vm, 1);
        mockHttp.VerifyMockHttpHandler("GET", "http://test.carlton.com/clients/50/users/107");
    }

    [Fact]
    public async Task Dispatch_WithInvalidHttpUrl_ThrowsInvalidArgumentException()
    {
        //Arrange
        var expectedMessage = "The HTTP refresh endpoint is invalid (Parameter 'HttpRefreshAttribute')";
        var sender = new HttpRefreshWithInvalidHttpUrlCaller();
        var query = new ViewModelQuery();
        var sut = _fixture.Create<ViewModelHttpDecorator<TestState>>();

        //Act 
        var ex = await Assert.ThrowsAsync<ArgumentException>(async () => await sut.Dispatch<TestViewModel1>(sender, query, CancellationToken.None));

        //Assert
        Assert.Equal(expectedMessage, ex.Message);
        Assert.Equal(nameof(HttpRefreshAttribute), ex.ParamName);
    }

    [Fact]
    public async Task Dispatch_WithInvalidComponentUrlParameters_ThrowsInvalidArgumentException()
    {
        //Arrange
        var expectedMessage = "The HTTP refresh endpoint is invalid, following URL parameters were not replaced: {ClientID}, {UserID} (Parameter 'HttpRefreshParameterAttribute')";
        var sender = new HttpRefreshWithInvalidParametersCaller();
        var query = new ViewModelQuery();
        var sut = _fixture.Create<ViewModelHttpDecorator<TestState>>();

        //Act 
        var ex = await Assert.ThrowsAsync<ArgumentException>(async () => await sut.Dispatch<TestViewModel1>(sender, query, CancellationToken.None));

        //Assert
        Assert.Equal(expectedMessage, ex.Message);
        Assert.Equal(nameof(HttpRefreshParameterAttribute), ex.ParamName);
    }

    [Fact]
    public async Task Dispatch_NoAttribute_HttpRefreshNotCalled()
    {
        //Arrange
        var sender = new NoRefreshCaller();
        var query = new ViewModelQuery();
        var expextedResult = _fixture.Create<TestViewModel1>();
        var sut = _fixture.Create<ViewModelHttpDecorator<TestState>>();
        _decorated.SetupDispatcher(expextedResult);

        //Act 
        var actualResult = await sut.Dispatch<TestViewModel1>(sender, query, CancellationToken.None);

        //Assert
        Assert.False(mockHttp.InvokedRequests.Any());
        Assert.Equal(expextedResult, actualResult);
        _decorated.VerifyDispatch<TestViewModel1>(query);
        _fluxState.VerifyStateMutation(query, 0);
    }

    [Fact]
    public async Task Dispatch_WithNeverAttribute_HttpRefreshNotCalled()
    {
        //Arrange
        var sender = new HttpNeverRefreshCaller();
        var query = new ViewModelQuery();
        var expextedResult = _fixture.Create<TestViewModel1>();
        var sut = _fixture.Create<ViewModelHttpDecorator<TestState>>();
        _decorated.SetupDispatcher(expextedResult);

        //Act 
        var actualResult = await sut.Dispatch<TestViewModel1>(sender, query, CancellationToken.None);

        //Assert
        Assert.False(mockHttp.InvokedRequests.Any());
        Assert.Equal(expextedResult, actualResult);
        _decorated.VerifyDispatch<TestViewModel1>(query);
        _fluxState.VerifyStateMutation(query, 0);
    }

    [Theory]
    [MemberData(nameof(TestDataGenerator.GetViewModelData), MemberType = typeof(TestDataGenerator))]
    public async Task Dispatch_AssertViewModels<TViewModel>(TViewModel expectedViewModel)
    {
        //Arrange
        mockHttp.SetupMockHttpHandler("GET", "http://test.carlton.com/", 200, expectedViewModel);
        var sender = new HttpRefreshCaller();
        var query = new ViewModelQuery();
        var sut = _fixture.Create<ViewModelHttpDecorator<TestState>>();
        _decorated.SetupDispatcher(expectedViewModel);

        //Act 
        var actualViewModel = await sut.Dispatch<TViewModel>(sender, query, CancellationToken.None);

        //Assert
        Assert.Equal(expectedViewModel, actualViewModel);
    }
}

