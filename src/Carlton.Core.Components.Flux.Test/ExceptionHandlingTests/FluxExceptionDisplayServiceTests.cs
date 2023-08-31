using AutoFixture.AutoMoq;
using Carlton.Core.Components.Flux.ExceptionHandling;
using Carlton.Core.Components.Flux.Exceptions;
using Carlton.Core.Components.Flux.Models;

namespace Carlton.Core.Components.Flux.Test.ExceptionHandlingTests;

public class FluxExceptionDisplayServiceTests
{
    private readonly IFixture _fixture;

    public FluxExceptionDisplayServiceTests()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
    }

    [Fact]
    public void LogException_UnhandledException_ShouldBeLogged()
    {
        //Arrange
        var expectedResult = new ExceptionErrorPrompt
                            (
                               "Error",
                               "Oops! We are sorry an error has occurred. Please try again.",
                               "mdi-alert-circle-outline"
                            );
        var exception = new Exception();
        var sut = _fixture.Create<FluxExceptionDisplayService>();

        //Act
        var actualResult = sut.GetExceptionErrorPrompt(exception);


        //Assert
        Assert.Equal(expectedResult, actualResult);
    }

    [Fact]
    public void LogException_FluxException_ShouldNotBeLogged()
    {
        //Arrange
        var exception = _fixture.Create<FluxException>();
        var expectedResult = new ExceptionErrorPrompt
                    (
                        "Error",
                         exception.Message,
                         "mdi-alert-circle-outline"
                    );
        var sut = _fixture.Create<FluxExceptionDisplayService>();

        //Act
        var actualResult = sut.GetExceptionErrorPrompt(exception);


        //Assert
        Assert.Equal(expectedResult, actualResult);
    }
}
