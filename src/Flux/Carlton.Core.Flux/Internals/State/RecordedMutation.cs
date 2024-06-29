namespace Carlton.Core.Flux.Internals.State;

internal sealed record RecordedMutation<TState>(int MutationIndex, DateTime TimeStamp, string StateEvent, object Command, Func<TState, object, TState> MutationFunc);
