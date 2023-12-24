using AutoFixture.AutoMoq;
using Carlton.Core.Components.Flux.Contracts;
using Carlton.Core.Components.Flux.Handlers.ViewModels;
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

    public ViewModelValidationDecoratorTests()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
        _mockServiceProvider = _fixture.Freeze<Mock<IServiceProvider>>(); 
        _decorated = _fixture.Freeze<Mock<IViewModelQueryDispatcher<TestState>>>();
        _fixture.Freeze<Mock<ILogger<ViewModelValidationDecorator<TestState>>>>();
    }

    [Theory, AutoData]
    public async Task Dispatch_DispatchAndValidatorCalled_AssertViewModels(TestViewModel expectedResult)
    {
        //Arrange
        var sender = new object();
        var query = new ViewModelQuery();
        var validator = _fixture.Create<Mock<IValidator<TestViewModel>>>();
        var sut = _fixture.Create<ViewModelValidationDecorator<TestState>>();
        _mockServiceProvider.Setup(_ => _.GetService(typeof(IValidator<TestViewModel>))).Returns(validator.Object);
        _decorated.SetupDispatcher(expectedResult);

        //Act 
        var actualResult = await sut.Dispatch<TestViewModel>(sender, query, CancellationToken.None);

        //Assert
        validator.VerifyValidator();
        _decorated.VerifyDispatch<TestViewModel>(query);
        Assert.Equal(expectedResult, actualResult);
    }

    [Fact]
    public async Task Dispatch_ValidationFailure_ShouldThrowViewModelFluxException()
    {
        //Arrange
        var sender = new object();
        var query = new ViewModelQuery();
        var validator = _fixture.Create<Mock<IValidator<TestViewModel>>>();
        var sut = _fixture.Create<ViewModelValidationDecorator<TestState>>();
        _mockServiceProvider.Setup(_ => _.GetService(typeof(IValidator<TestViewModel>))).Returns(validator.Object);
        _decorated.SetupDispatcher(_fixture.Create<TestViewModel>());
        validator.SetupValidationFailure();
 
        //Act 
        var ex = await Assert.ThrowsAsync<ViewModelFluxException<TestState, TestViewModel>>(async () => await sut.Dispatch<TestViewModel>(sender, query, CancellationToken.None));

        //Assert
        Assert.Equal(LogEvents.ViewModel_Validation_Error, ex.EventID);
        Assert.Equal(LogEvents.ViewModel_Validation_ErrorMsg, ex.Message);
    }
}
