//using AutoFixture.AutoMoq;
//using Carlton.Core.Flux.Contracts;
//using Carlton.Core.Flux.Handlers.ViewModels;
//using Carlton.Core.Flux.Models;
//using Carlton.Core.Flux.Tests.Common;
//using Carlton.Core.Flux.Tests.Common.Extensions;
//namespace Carlton.Core.Flux.Tests.HandlerTests;

//public class ViewModelQueryHandlerTests
//{
//    private readonly IFixture _fixture;
//    private readonly TestState _testState;
//    private readonly Mock<IFluxState<TestState>> _mockFluxState;
//    private readonly Mock<IMapper> _mockMapper;

//    public ViewModelQueryHandlerTests()
//    {
//        _fixture = new Fixture().Customize(new AutoMoqCustomization());
//        _testState = _fixture.Create<TestState>();
//        _mockFluxState = _fixture.Freeze<Mock<IFluxState<TestState>>>();
//        _mockMapper = _fixture.Freeze<Mock<IMapper>>();
//        _mockFluxState.Setup(_ => _.State).Returns(_testState);
//    }

//    [Fact]
//    public async Task Handle_ShouldCallStateMutation()
//    {
//        //Arrange
//        var query = new ViewModelQuery();
//        var sut = _fixture.Create<ViewModelQueryHandler<TestState>>();
//        var vm = _fixture.Create<TestViewModel>();
//        _mockMapper.SetupMapper(_testState, vm);

//        //Act
//        var result = await sut.Handle<TestViewModel>(query, CancellationToken.None);

//        //Assert
//        Assert.Equal(vm, result);
//        _mockMapper.VerifyMapper<TestViewModel>(1);
//    }
//}
