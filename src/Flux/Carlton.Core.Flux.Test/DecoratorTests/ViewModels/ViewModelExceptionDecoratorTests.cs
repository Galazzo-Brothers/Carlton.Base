//using AutoFixture.AutoMoq;
//using Carlton.Core.Flux.Contracts;
//using Carlton.Core.Flux.Handlers.ViewModels;
//using Carlton.Core.Flux.Models;
//using Carlton.Core.Flux.Tests.Common;
//using Carlton.Core.Flux.Tests.Common.Extensions;
//using Microsoft.Extensions.Logging;

//namespace Carlton.Core.Flux.Tests.DecoratorTests.ViewModels;

//public class ViewModelExceptionDecoratorTests
//{
//    private readonly IFixture _fixture;
//    private readonly Mock<IViewModelQueryDispatcher<TestState>> _decorated;

//    public ViewModelExceptionDecoratorTests()
//    {
//        _fixture = new Fixture().Customize(new AutoMoqCustomization());
//        _decorated = _fixture.Freeze<Mock<IViewModelQueryDispatcher<TestState>>>();
//        _fixture.Freeze<Mock<ILogger<ViewModelExceptionDecorator<TestState>>>>();
//    }

//    [Theory, AutoData]
//    public async Task Dispatch_DispatchCalled_AssertViewModels(TestViewModel expectedResult)
//    {
//        //Arrange
//        var sender = new JsRefreshCaller();
//        var query = new ViewModelQuery();
//        var sut = _fixture.Create<ViewModelExceptionDecorator<TestState>>();

//        _decorated.SetupDispatcher(expectedResult);

//        //Act 
//        var actualResult = await sut.Dispatch<TestViewModel>(sender, query, CancellationToken.None);

//        //Assert
//        _decorated.VerifyDispatch<TestViewModel>(query);
//        Assert.Equal(expectedResult, actualResult);
//    }

//    [Fact]
//    public async Task Dispatch_Errors_ThrowsViewModelFluxException()
//    {
//        //Arrange
//        var sender = new object();
//        _decorated.SetupDispatcherException<TestViewModel, Exception>(new Exception());
//        var query = new ViewModelQuery();
//        var sut = _fixture.Create<ViewModelExceptionDecorator<TestState>>();

//        //Act
//        var ex = await Assert.ThrowsAsync<ViewModelFluxException<TestState, TestViewModel>>(async () => await sut.Dispatch<TestViewModel>(sender, query, CancellationToken.None));

//        //Assert
//        Assert.Equal(LogEvents.ViewModel_Unhandled_Error, ex.EventID);
//        Assert.Equal(LogEvents.ViewModel_Unhandled_ErrorMsg, ex.Message);
//    }
//}
