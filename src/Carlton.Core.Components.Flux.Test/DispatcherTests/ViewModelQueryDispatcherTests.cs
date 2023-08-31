using AutoFixture.AutoMoq;
using Carlton.Core.Components.Flux.Contracts;
using Carlton.Core.Components.Flux.Dispatchers;
using Carlton.Core.Components.Flux.Models;
using Carlton.Core.Components.Flux.Test.Common;
using Carlton.Core.Components.Flux.Test.Common.Extensions;

namespace Carlton.Core.Components.Flux.Test.DispatcherTests;

public class ViewModelQueryDispatcherTests
{
    private readonly IFixture _fixture;

    public ViewModelQueryDispatcherTests()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
    }

    [Theory]
    [MemberData(nameof(TestDataGenerator.GetViewModelData), MemberType = typeof(TestDataGenerator))]
    public async Task Dispatch_AssertHandlerCalled_AssertViewModelResponse<TViewModel>(TViewModel expectedViewModel)
    {
        //Arrange
        var provider = _fixture.Freeze<Mock<IServiceProvider>>();
        var handler = _fixture.Freeze<Mock<IViewModelQueryHandler<TestState, TViewModel>>>();
        var sender = _fixture.Create<object>();
        var query = _fixture.Create<ViewModelQuery>();
        var sut = _fixture.Create<ViewModelQueryDispatcher<TestState>>();

        provider.SetupServiceProvider<IViewModelQueryHandler<TestState, TViewModel>>(handler.Object);
        handler.SetupHandler(expectedViewModel);

        //Act
        var result = await sut.Dispatch<TViewModel>(sender,query, CancellationToken.None);

        //Assert
        handler.VerifyHandler();
        Assert.Equal(expectedViewModel, result);
    }
}



