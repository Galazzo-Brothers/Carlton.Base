//using AutoFixture.AutoMoq;
//using Carlton.Core.Flux.Contracts;
//using Carlton.Core.Flux.Dispatchers;
//using Carlton.Core.Flux.Models;
//using Carlton.Core.Flux.Tests.Common;
//using Carlton.Core.Flux.Tests.Common.Extensions;

//namespace Carlton.Core.Flux.Tests.DispatcherTests;

//public class MutationCommandDispatcherTests
//{
//    private readonly IFixture _fixture;

//    public MutationCommandDispatcherTests()
//    {
//        _fixture = new Fixture().Customize(new AutoMoqCustomization());
//    }

//    [Theory, AutoData]
//    public async Task Dispatch_AssertHandlerCalled(MutationCommand command)
//    {
//        //Arrange
//        var serviceProvider = _fixture.Freeze<Mock<IServiceProvider>>();
//        var handler = _fixture.Freeze<Mock<IMutationCommandHandler<TestState>>>();
//        var sender = _fixture.Create<object>();
//        var sut = _fixture.Create<MutationCommandDispatcher<TestState>>();

//        serviceProvider.SetupServiceProvider<IMutationCommandHandler<TestState>>(handler.Object);
//        handler.SetupHandler<MutationCommand>();
      

//        //Act
//        await sut.Dispatch(sender, command, CancellationToken.None);

//        //Assert
//        handler.VerifyHandler<MutationCommand>();
//    }  
//}
