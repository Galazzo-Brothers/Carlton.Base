using AutoFixture.AutoMoq;
using Carlton.Core.Components.Flux.Contracts;
using Carlton.Core.Components.Flux.Decorators.Queries;
using Carlton.Core.Components.Flux.Models;
using Carlton.Core.Components.Flux.Test.Common;
using Carlton.Core.Components.Flux.Test.Common.Extensions;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Carlton.Core.Components.Flux.Test.DecoratorTests.ViewModels;

public class ViewModelValidationDecoratorTests
{
    private readonly IFixture _fixture;
    private readonly Mock<IServiceProvider> _mockServiceProvider;
    private readonly Mock<IViewModelQueryDispatcher<TestState>> _decorated;
    private readonly Mock<ILogger<ViewModelValidationDecorator<TestState>>> _logger = new();

    public ViewModelValidationDecoratorTests()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
        _mockServiceProvider = _fixture.Freeze<Mock<IServiceProvider>>(); 
        _decorated = _fixture.Freeze<Mock<IViewModelQueryDispatcher<TestState>>>();
        _logger = _fixture.Freeze<Mock<ILogger<ViewModelValidationDecorator<TestState>>>>();
    }

    [Theory]
    [MemberData(nameof(TestDataGenerator.GetViewModelData), MemberType = typeof(TestDataGenerator))]
    public async Task Dispatch_DispatchAndValidatorCalled_AssertViewModels<TViewModel>(TViewModel expectedResult)
    {
        //Arrange
        var sender = new object();
        var query = new ViewModelQuery();
        var validator = _fixture.Create<Mock<IValidator<TViewModel>>>();
        var sut = _fixture.Create<ViewModelValidationDecorator<TestState>>();

        _mockServiceProvider.Setup(_ => _.GetService(typeof(IValidator<TViewModel>))).Returns(validator.Object);
        _decorated.SetupDispatcher(expectedResult);

        //Act 
        var actualResult = await sut.Dispatch<TViewModel>(sender, query, CancellationToken.None);

        //Assert
        validator.VerifyValidator();
        _decorated.VerifyDispatch<TViewModel>(query);
        Assert.Equal(expectedResult, actualResult);
    }
}
