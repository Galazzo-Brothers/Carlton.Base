using AutoFixture.AutoMoq;
using Carlton.Core.Components.Flux.Contracts;
using Carlton.Core.Components.Flux.Decorators.Commands;
using Carlton.Core.Components.Flux.Models;
using Carlton.Core.Components.Flux.Test.Common;
using Carlton.Core.Components.Flux.Test.Common.Extensions;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Carlton.Core.Components.Flux.Test.DecoratorTests.Mutations;

public class MutationValidationDecoratorTests
{
    private readonly IFixture _fixture;
    private readonly Mock<IServiceProvider> _serviceProvider;
    private readonly Mock<IMutationCommandDispatcher<TestState>> _decorated;
    private readonly Mock<ILogger<MutationValidationDecorator<TestState>>> _logger;

    public MutationValidationDecoratorTests()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
        _serviceProvider = _fixture.Freeze<Mock<IServiceProvider>>();
        _decorated = _fixture.Freeze<Mock<IMutationCommandDispatcher<TestState>>>();
        _logger = _fixture.Freeze<Mock<ILogger<MutationValidationDecorator<TestState>>>>();
    }

    [Theory]
    [MemberData(nameof(TestDataGenerator.GetCommandData), MemberType = typeof(TestDataGenerator))]
    public async Task Dispatch_DispatchAndValidateCalled<TCommand>(TCommand command)
        where TCommand : MutationCommand
    {
        //Arrange
        var sender = new object();
        var validator = _fixture.Create<Mock<IValidator<TCommand>>>();
        var sut = _fixture.Create<MutationValidationDecorator<TestState>>();

        _serviceProvider.SetupServiceProvider<IValidator<TCommand>>(validator.Object);

        //Act 
        await sut.Dispatch(sender,command, CancellationToken.None);

        //Assert
        validator.VerifyValidator();
        _decorated.VerifyDispatchCalled(command);
    }
}
