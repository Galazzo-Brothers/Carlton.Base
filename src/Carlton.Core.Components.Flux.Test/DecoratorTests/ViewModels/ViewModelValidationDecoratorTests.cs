using Carlton.Core.Components.Flux.Contracts;
using Carlton.Core.Components.Flux.Decorators.Queries;
using Carlton.Core.Components.Flux.Models;
using Carlton.Core.Components.Flux.Test.Common;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;

namespace Carlton.Core.Components.Flux.Test.DecoratorTests.ViewModels;

public class ViewModelValidationDecoratorTests
{
    private readonly Mock<IServiceProvider> _mockServiceProvider = new();
    private readonly Mock<IValidator<TestViewModel1>> _validator1 = new();
    private readonly Mock<IValidator<TestViewModel2>> _validator2 = new();
    private readonly Mock<IViewModelQueryDispatcher<TestState>> _decorated = new();
    private readonly Mock<ILogger<ViewModelValidationDecorator<TestState>>> _logger = new();
    private readonly ViewModelValidationDecorator<TestState> _dispatcher;

    public ViewModelValidationDecoratorTests()
    {
        _mockServiceProvider.Setup(_ => _.GetService(typeof(Mock<IValidator<TestViewModel1>>))).Returns(_validator1);
        _mockServiceProvider.Setup(_ => _.GetService(typeof(Mock<IValidator<TestViewModel2>>))).Returns(_validator2);
        _mockServiceProvider.Setup(_ => _.GetService(typeof(IValidator<TestViewModel1>))).Returns(_validator1.Object);
        _mockServiceProvider.Setup(_ => _.GetService(typeof(IValidator<TestViewModel2>))).Returns(_validator2.Object);
        _dispatcher = new ViewModelValidationDecorator<TestState>(_decorated.Object, _mockServiceProvider.Object, _logger.Object);
    }

    [Theory]
    [MemberData(nameof(TestDataGenerator.GetViewModelData), MemberType = typeof(TestDataGenerator))]
    public async Task Dispatch_DispatchAndValidatorCalled<TViewModel>(TViewModel vm)
    {
        //Arrange
        var sender = new object();
        var query = new ViewModelQuery();
        var validator = _mockServiceProvider.Object.GetRequiredService<Mock<IValidator<TViewModel>>>();

        //Act 
        await _dispatcher.Dispatch<TViewModel>(sender,query, CancellationToken.None);

        //Assert
        validator.VerifyValidator();
        _decorated.VerifyDispatch<TViewModel>(query);
    }

    [Theory]
    [MemberData(nameof(TestDataGenerator.GetViewModelData), MemberType = typeof(TestDataGenerator))]
    public async Task Dispatch_AssertViewModels<TViewModel>(TViewModel expectedViewModel)
    {
        //Arrange
        var sender = new object();
        var query = new ViewModelQuery();
        _decorated.SetupDispatcher(expectedViewModel);

        //Act 
        var actualViewModel = await _dispatcher.Dispatch<TViewModel>(sender,query, CancellationToken.None);

        //Assert
        Assert.Equal(expectedViewModel, actualViewModel);
    }
}
