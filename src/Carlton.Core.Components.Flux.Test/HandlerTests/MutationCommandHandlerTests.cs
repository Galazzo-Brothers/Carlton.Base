using AutoFixture.AutoMoq;
using Carlton.Core.Components.Flux.Contracts;
using Carlton.Core.Components.Flux.Handlers;
using Carlton.Core.Components.Flux.Test.Common;
using Carlton.Core.Components.Flux.Test.Common.Extensions;

namespace Carlton.Core.Components.Flux.Test.HandlerTests;

public class MutationCommandHandlerTests
{
    private readonly IFixture _fixture;
    private readonly Mock<IMutableFluxState<TestState>> _fluxState;

    public MutationCommandHandlerTests()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
        _fluxState = _fixture.Freeze<Mock<IMutableFluxState<TestState>>>();
    }

    [Fact]
    public async Task Handle_ShouldCallStateMutation()
    {
        //Arrange?
        var command = _fixture.Create<TestCommand1>();
        var sut = _fixture.Create<MutationCommandHandler<TestState>>();

        //Act
        await sut.Handle(command, CancellationToken.None);

        //Assert
        _fluxState.VerifyStateMutation(command, 1);
    }
}
