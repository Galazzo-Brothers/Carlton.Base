using AutoFixture.AutoMoq;
using Carlton.Core.Components.Flux.Exceptions;
using Carlton.Core.Components.Flux.Exceptions.ExceptionHandling;
using Carlton.Core.Components.Flux.Test.ComponentTests;
using Microsoft.Extensions.Logging;

namespace Carlton.Core.Components.Flux.Test.ExceptionHandlingTests;

public class ComponentExceptionLoggingServiceTests
{
    private readonly IFixture _fixture;
    private readonly Mock<ILogger> _logger;

    public ComponentExceptionLoggingServiceTests()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
        _logger = _fixture.Create<Mock<ILogger>>();
    }

    [Fact]
    public void LogException_UnhandledException_ShouldBeLogged()
    {
        //Arrange
        var type = typeof(DummyConnectedComponent);
        var exception = new Exception();
        var sut = _fixture.Create<ComponentExceptionLoggingService>();

        //Act
        sut.LogException(_logger.Object, exception, type);


        //Assert
        Assert.True(_logger.Invocations.Any());
    }

    [Fact]
    public void LogException_FluxException_ShouldNotBeLogged()
    {
        //Arrange
        var type = typeof(DummyConnectedComponent);
        var exception = _fixture.Create<FluxException>();
        var sut = _fixture.Create<ComponentExceptionLoggingService>();

        //Act
        sut.LogException(_logger.Object, exception, type);


        //Assert
        Assert.False(_logger.Invocations.Any());
    }
}
