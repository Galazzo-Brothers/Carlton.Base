using Carlton.Core.Components.Flux.Contracts;

namespace Carlton.Core.Components.Flux.Test.Common.Extensions;

public static class MockFluxStateMutationExtensions
{
    public static void SetUpMutationResolver<TMutation>(this Mock<IMutationResolver<TestState>> mutationResolver, IFluxStateMutation<TestState, TMutation> mutation)
    {
        mutationResolver.Setup(_ => _.Resolve<TMutation>()).Returns(mutation);
    }
    
    public static void SetUpMutation<T>(this Mock<IFluxStateMutation<TestState, T>> mutation, string stateEvent, bool isRefreshMutation)
    {
        mutation.Setup(_ => _.StateEvent).Returns(stateEvent);
        mutation.Setup(_ => _.IsRefreshMutation).Returns(isRefreshMutation);
    }
    
    public static void VerifyMutation<T>(this Mock<IFluxStateMutation<TestState, T>> mutation, TestState state, T input)
    {
        mutation.Verify(_ => _.Mutate(state, input), Times.Once);
    }
    
    public static void VerifyStateMutation<T>(this Mock<IMutableFluxState<TestState>> mutation, T input, int times)
    {
        mutation.Verify(_ => _.MutateState(input), Times.Exactly(times));
    }
}

