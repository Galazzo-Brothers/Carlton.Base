using Carlton.Core.Flux.Contracts;
namespace Carlton.Core.Flux.Tests.Common.Extensions;

public static class FluxStateMutationExtensions
{
    //public static void SetUpMutationResolver<TMutation>(this Mock<IMutationResolver<TestState>> mutationResolver, IFluxStateMutation<TestState, TMutation> mutation)
    //{
    //    mutationResolver.Setup(_ => _.Resolve<TMutation>()).Returns(mutation);
    //}

    //public static void SetUpMutation<T>(this Mock<IFluxStateMutation<TestState, T>> mutation, string stateEvent, bool isRefreshMutation)
    //{
    //    mutation.Setup(_ => _.StateEvent).Returns(stateEvent);
    //    mutation.Setup(_ => _.IsRefreshMutation).Returns(isRefreshMutation);
    //}

    //public static void VerifyMutation<T>(this IFluxStateMutation<TestState, T> mutation, TestState state, T input)
    //{
    //    mutation.Received(1).Mutate(state, input);
    //}

    //public static void VerifyStateMutation<T>(this IMutableFluxState<TestState> state, T command)
    //{
    //    state.Received(1).ApplyMutationCommand(command);
    //}
}

