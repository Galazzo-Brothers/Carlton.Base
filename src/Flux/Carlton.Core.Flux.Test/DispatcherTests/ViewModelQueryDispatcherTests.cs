//using AutoFixture.AutoMoq;
//using Carlton.Core.Flux.Contracts;
//using Carlton.Core.Flux.Dispatchers;
//using Carlton.Core.Flux.Models;
//using Carlton.Core.Flux.Tests.Common;
//using Carlton.Core.Flux.Tests.Common.Extensions;

//namespace Carlton.Core.Flux.Tests.DispatcherTests;

//public class ViewModelQueryDispatcherTests
//{
//    private readonly IFixture _fixture;

//    public ViewModelQueryDispatcherTests()
//    {
//        _fixture = new Fixture().Customize(new AutoMoqCustomization());
//    }

//    [Theory, AutoData]
//    public async Task Dispatch_AssertHandlerCalled_AssertViewModelResponse(TestViewModel expectedViewModel)
//    {
//        //Arrange
//        var provider = _fixture.Freeze<Mock<IServiceProvider>>();
//        var handler = _fixture.Freeze<Mock<IViewModelQueryHandler<TestState>>>();
//        var sender = _fixture.Create<object>();
//        var query = _fixture.Create<ViewModelQuery>();
//        var sut = _fixture.Create<ViewModelQueryDispatcher<TestState>>();
//        provider.SetupServiceProvider<IViewModelQueryHandler<TestState>>(handler.Object);
//        handler.SetupHandler(expectedViewModel);

//        //Act
//        var result = await sut.Dispatch<TestViewModel>(sender,query, CancellationToken.None);

//        //Assert
//        handler.VerifyHandler<TestViewModel>();
//        Assert.Equal(expectedViewModel, result);
//    }
//}



