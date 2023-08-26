using Carlton.Core.Components.Flux.Contracts;
using Carlton.Core.Components.Flux.Decorators.Commands;
using Carlton.Core.Components.Flux.Models;
using Carlton.Core.Components.Flux.Test.Common;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using System.Linq.Expressions;

namespace Carlton.Core.Components.Flux.Test.DecoratorTests.Mutations;

public class MutationValidationDecoratorTests
{
    private readonly Mock<IServiceProvider> _mockServiceProvider = new();
    private readonly Mock<IValidator<TestCommand1>> _validator1 = new();
    private readonly Mock<IValidator<TestCommand2>> _validator2 = new();
    private readonly Mock<IMutationCommandDispatcher<TestState>> _decorated = new();
    private readonly Mock<ILogger<MutationValidationDecorator<TestState>>> _logger = new();
    private readonly MutationValidationDecorator<TestState> _dispatcher;

    public MutationValidationDecoratorTests()
    {
        _mockServiceProvider.Setup(_ => _.GetService(typeof(Mock<IValidator<TestCommand1>>))).Returns(_validator1);
        _mockServiceProvider.Setup(_ => _.GetService(typeof(Mock<IValidator<TestCommand2>>))).Returns(_validator2);
        _mockServiceProvider.Setup(_ => _.GetService(typeof(IValidator<TestCommand1>))).Returns(_validator1.Object);
        _mockServiceProvider.Setup(_ => _.GetService(typeof(IValidator<TestCommand2>))).Returns(_validator2.Object);
        _dispatcher = new MutationValidationDecorator<TestState>(_decorated.Object, _mockServiceProvider.Object, _logger.Object);
    }

    [Theory]
    [MemberData(nameof(TestDataGenerator.GetCommandData), MemberType = typeof(TestDataGenerator))]
    public async Task Dispatch_DispatchAndValidatorCalled<TCommand>(TCommand command)
        where TCommand : MutationCommand
    {
        //Arrange
        var validator = _mockServiceProvider.Object.GetRequiredService<Mock<IValidator<TCommand>>>();
        var query = new ViewModelQuery(this);

        //Act 
        await _dispatcher.Dispatch(command, CancellationToken.None);

        //Assert
        validator.Verify(GetValidationExpression<TCommand>(), Times.Once);
        _decorated.VerifyDispatchCalled(command);
    }

    private static Expression<Func<IValidator<TViewModel>, ValidationResult>> GetValidationExpression<TViewModel>()
    {
        Expression<Func<IValidator<TViewModel>, ValidationResult>> result =
            mock => mock.Validate(It.IsAny<ValidationContext<TViewModel>>());

        return result;
    }
}
