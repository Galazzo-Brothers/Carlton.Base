using Carlton.Core.Components.Flux.Contracts;
using Carlton.Core.Components.Flux.Decorators.Commands;
using Carlton.Core.Components.Flux.Models;
using Carlton.Core.Components.Flux.Test.Common;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;

namespace Carlton.Core.Components.Flux.Test.DecoratorTests.Mutations;

public class MutationValidationDecoratorTests
{
    private readonly Mock<IServiceProvider> _mockServiceProvider = new();
    private readonly Mock<IMutationCommandDispatcher<TestState>> _decorated = new();
    private readonly Mock<ILogger<MutationValidationDecorator<TestState>>> _logger = new();
    private readonly MutationValidationDecorator<TestState> _dispatcher;

    public MutationValidationDecoratorTests()
    {
        _dispatcher = new MutationValidationDecorator<TestState>(_decorated.Object, _mockServiceProvider.Object, _logger.Object);
    }

    [Theory]
    [MemberData(nameof(TestDataGenerator.GetCommandData), MemberType = typeof(TestDataGenerator))]
    public async Task Dispatch_DispatchAndValidateCalled<TCommand>(TCommand command)
        where TCommand : MutationCommand
    {
        //Arrange
        var sender = new object();
        var validator =new Mock<IValidator<TCommand>>();
        _mockServiceProvider.SetupServiceProvider<IValidator<TCommand>>(validator.Object);

        //Act 
        await _dispatcher.Dispatch(sender,command, CancellationToken.None);

        //Assert
        validator.VerifyValidator();
        _decorated.VerifyDispatchCalled(command);
    }
}
