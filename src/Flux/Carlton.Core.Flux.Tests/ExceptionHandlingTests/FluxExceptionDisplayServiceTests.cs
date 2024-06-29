//using AutoFixture.AutoMoq;
//using Carlton.Core.Flux.Exceptions;
//using Carlton.Core.Flux.Exceptions.ExceptionHandling;
//using Carlton.Core.Flux.Models;

//namespace Carlton.Core.Flux.Tests.ExceptionHandlingTests;

//public class FluxExceptionDisplayServiceTests
//{
//    private readonly IFixture _fixture;

//    public FluxExceptionDisplayServiceTests()
//    {
//        _fixture = new Fixture().Customize(new AutoMoqCustomization());
//    }

//    [Fact]
//    public void LogException_UnhandledException_ShouldBeLogged()
//    {
//        //Arrange
//        var action = () => { };
//        var expectedResult = new ExceptionErrorPrompt
//                            (
//                               "Error",
//                               "Oops! We are sorry an error has occurred. Please try again.",
//                               "mdi-alert-circle-outline",
//                               action
//                            );
//        var exception = new Exception();
//        var sut = _fixture.Create<FluxExceptionDisplayService>();

//        //Act
//        var actualResult = sut.GetExceptionErrorPrompt(exception, action);


//        //Assert
//        Assert.Equal(expectedResult, actualResult);
//    }

//    [Fact]
//    public void LogException_FluxException_ShouldNotBeLogged()
//    {
//        //Arrange
//        var action = () => { };
//        var exception = _fixture.Create<FluxException>();
//        var expectedResult = new ExceptionErrorPrompt
//                    (
//                        "Error",
//                         exception.Message,
//                         "mdi-alert-circle-outline",
//                         action
//                    );
//        var sut = _fixture.Create<FluxExceptionDisplayService>();

//        //Act
//        var actualResult = sut.GetExceptionErrorPrompt(exception, action);


//        //Assert
//        Assert.Equal(expectedResult, actualResult);
//    }
//}
