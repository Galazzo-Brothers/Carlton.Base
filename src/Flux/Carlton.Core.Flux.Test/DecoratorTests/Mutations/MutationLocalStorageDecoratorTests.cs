//using AutoFixture.AutoMoq;
//using Blazored.LocalStorage;
//using Carlton.Core.Flux.Contracts;
//using Carlton.Core.Flux.Handlers.Mutations;
//using Carlton.Core.Flux.Models;
//using Carlton.Core.Flux.Tests.Common;
//using Carlton.Core.Flux.Tests.Common.Extensions;
//using Carlton.Core.Utilities.Logging;
//using Microsoft.Extensions.Logging;
//using System.Text.Json;
//namespace Carlton.Core.Flux.Tests.DecoratorTests.Mutations;

//public class MutationLocalStorageDecoratorTests
//{
//    private readonly IFixture _fixture;
//    private readonly Mock<IMutationCommandDispatcher<TestState>> _decorated;
//    private readonly Mock<ILocalStorageService> _localStorage;

//    public MutationLocalStorageDecoratorTests()
//    {
//        _fixture = new Fixture().Customize(new AutoMoqCustomization());
//        _decorated = _fixture.Freeze<Mock<IMutationCommandDispatcher<TestState>>>();
//        _localStorage = _fixture.Freeze<Mock<ILocalStorageService>>();
//        _fixture.Freeze<Mock<ILogger<MutationLocalStorageDecorator<TestState>>>>();
//        _fixture.Freeze<Mock<InMemoryLogger>>();
//        _fixture.Freeze<IFluxState<TestState>>();
//    }

//    [Theory, AutoData]
//    public async Task Dispatch_DispatchAndLocalStorageCalled(MutationCommand command)
//    {
//        //Arrange
//        var sender = new object();  
//        var sut = _fixture.Create<MutationLocalStorageDecorator<TestState>>();

//        //Act 
//        await sut.Dispatch(sender, command, CancellationToken.None);

//        //Assert
//        _decorated.VerifyDispatchCalled(command);
//        _localStorage.VerifyLocalStorageCalled<TestState>("carltonFluxState");
//        _localStorage.VerifyLocalStorageCalled<LogMessage[]>("carltonFluxLogs");
//    }

//    [Theory, AutoData]
//    public async Task Dispatch_DispatchWithJsonException_ShouldThrowMutationCommandFluxException(MutationCommand command)
//    {
//        //Arrange
//        var sender = new object();
//        var sut = _fixture.Create<MutationLocalStorageDecorator<TestState>>();
//        _decorated.SetupDispatcherException(new JsonException());

//        //Act 
//        var ex = await Assert.ThrowsAsync<MutationCommandFluxException<TestState, MutationCommand>>(
//            async () => await sut.Dispatch(sender, command, CancellationToken.None));

//        //Assert
//        Assert.Equal(LogEvents.Mutation_LocalStorage_JSON_Error, ex.EventID);
//        Assert.Equal(LogEvents.Mutation_LocalStorage_JSON_ErrorMsg, ex.Message);
//    }

//    [Theory, AutoData]
//    public async Task Dispatch_DispatchWithJsonNotSupportedException_ShouldThrowMutationCommandFluxException(MutationCommand command)
//    {
//        //Arrange
//        var sender = new object();
//        var sut = _fixture.Create<MutationLocalStorageDecorator<TestState>>();
//        _localStorage.SetupLocalStorageException(new NotSupportedException("Serialization and deserialization"));

//        //Act 
//        var ex = await Assert.ThrowsAsync<MutationCommandFluxException<TestState, MutationCommand>>(
//            async () => await sut.Dispatch(sender, command, CancellationToken.None));

//        //Assert
//        Assert.Equal(LogEvents.Mutation_LocalStorage_JSON_Error, ex.EventID);
//        Assert.Equal(LogEvents.Mutation_LocalStorage_JSON_ErrorMsg, ex.Message);
//    }
//}
