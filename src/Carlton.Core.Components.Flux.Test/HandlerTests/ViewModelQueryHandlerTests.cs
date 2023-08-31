using AutoFixture.AutoMoq;
using Carlton.Core.Components.Flux.Contracts;
using Carlton.Core.Components.Flux.Handlers;
using Carlton.Core.Components.Flux.Models;
using Carlton.Core.Components.Flux.Test.Common;
using Carlton.Core.Components.Flux.Test.Common.Extensions;
using MapsterMapper;

namespace Carlton.Core.Components.Flux.Test.HandlerTests;

public class ViewModelQueryHandlerTests
{
    private readonly IFixture _fixture;
    private readonly TestState _testState;
    private readonly Mock<IFluxState<TestState>> _mockFluxState;
    private readonly Mock<IMapper> _mockMapper;

    public ViewModelQueryHandlerTests()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
        _testState = _fixture.Create<TestState>();
        _mockFluxState = _fixture.Freeze<Mock<IFluxState<TestState>>>();
        _mockMapper = _fixture.Freeze<Mock<IMapper>>();
        _mockFluxState.Setup(_ => _.State).Returns(_testState);
    }

    [Fact]
    public async Task Handle_ShouldCallStateMutation()
    {
        //Arrange
        var query = new ViewModelQuery();
        var sut = _fixture.Create<ViewModelQueryHandler<TestState, TestViewModel1>>();
        var vm = _fixture.Create<TestViewModel1>();
        _mockMapper.SetupMapper(_testState, vm);

        //Act
        var result = await sut.Handle(query, CancellationToken.None);

        //Assert
        Assert.Equal(vm, result);
        _mockMapper.VerifyMapper<TestViewModel1>(1);
    }
}
