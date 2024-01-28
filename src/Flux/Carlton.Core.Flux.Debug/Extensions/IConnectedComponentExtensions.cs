namespace Carlton.Core.Flux.Debug.Extensions;

public static class ConnectedComponentExtensions
{
    public static async Task RaiseComponentFluxEvent<TViewModel, TCommand>(this IConnectedComponent<TViewModel> component, object args)
    {
        var command = MutationCommandMapper.Map<TCommand>(args);
        await component.OnComponentEvent.InvokeAsync(command);
    }
}

