//using AutoFixture.AutoMoq;
//using Carlton.Core.Flux.Contracts;
//using Carlton.Core.Flux.Handlers.Mutations;
//using Carlton.Core.Flux.Models;
//using Carlton.Core.Flux.Tests.Common;
//using Carlton.Core.Flux.Tests.Common.Extensions;
//using FluentValidation;
//using Microsoft.Extensions.Logging;

//namespace Carlton.Core.Flux.Tests.DecoratorTests.Mutations;

//public class MutationValidationDecoratorTests
//{
//    private readonly IFixture _fixture;
//    private readonly Mock<IServiceProvider> _serviceProvider;
//    private readonly Mock<IValidator<MutationCommand>> _validator;
//    private readonly Mock<IMutationCommandDispatcher<TestState>> _decorated;

//    public MutationValidationDecoratorTests()
//    {
//        _fixture = new Fixture().Customize(new AutoMoqCustomization());
//        _serviceProvider = _fixture.Freeze<Mock<IServiceProvider>>();
//        _validator = _fixture.Freeze<Mock<IValidator<MutationCommand>>>();
//        _decorated = _fixture.Freeze<Mock<IMutationCommandDispatcher<TestState>>>();
//        _fixture.Freeze<Mock<ILogger<MutationValidationDecorator<TestState>>>>();
//    }

//    [Theory, AutoData]
//    public async Task Dispatch_DispatchAndValidateCalled(MutationCommand command)
//    {
//        //Arrange
//        var sender = new object();
//        var sut = _fixture.Create<MutationValidationDecorator<TestState>>();
//        _serviceProvider.SetupServiceProvider<IValidator<MutationCommand>>(_validator.Object);

//        //Act 
//        await sut.Dispatch(sender, command, CancellationToken.None);

//        //Assert
//        _validator.VerifyValidator();
//        _decorated.VerifyDispatchCalled(command);
//    }

//    [Theory, AutoData]
//    public async Task Dispatch_InvalidQuery_ThrowsMutationCommandFluxExceptionException(MutationCommand command)
//    {
//        //Arrange
//        var sender = new object();
//        var sut = _fixture.Create<MutationValidationDecorator<TestState>>();
//        _validator.SetupValidationFailure();
//        _serviceProvider.SetupServiceProvider<IValidator<MutationCommand>>(_validator.Object);

//        //Act 
//        var ex = await Assert.ThrowsAsync<MutationCommandFluxException<TestState, MutationCommand>>(async () => await sut.Dispatch(sender, command, CancellationToken.None));

//        //Assert
//        Assert.Equal(LogEvents.Mutation_Validation_ErrorMsg, ex.Message);
//        Assert.Equal(LogEvents.Mutation_Validation_Error, ex.EventID);
//    }
//}
