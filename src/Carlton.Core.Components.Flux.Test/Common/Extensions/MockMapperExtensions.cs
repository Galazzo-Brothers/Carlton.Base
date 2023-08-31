using MapsterMapper;

namespace Carlton.Core.Components.Flux.Test.Common.Extensions;

public static class MockMapperExtensions
{
    public static void SetupMapper<TViewModel>(this Mock<IMapper> mapper, object source, TViewModel destination)
    {
        mapper.Setup(_ => _.Map<TViewModel>(source)).Returns(destination);
    }

    public static void VerifyMapper<TViewModel>(this Mock<IMapper> mapper, int times)
    {
        mapper.Verify(_ => _.Map<TViewModel>(It.IsAny<TestState>()), Times.Exactly(times));
    }

    public static void VerifyMapper(this Mock<IMapper> mapper, int times)
    {
        mapper.Verify(_ => _.Map(It.IsAny<TestState>(), It.IsAny<TestState>()), Times.Exactly(times));
    }
}


