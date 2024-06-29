namespace Carlton.Core.Lab.Extensions;

internal static class ConnectedComponentExtensions
{
	internal static async Task RaiseComponentFluxEvent<TViewModel, TCommand>(this IConnectedComponent<TViewModel> component, object args)
	{
		var command = MutationCommandMapper.Map<TCommand>(args);
		await component.OnComponentEvent.InvokeAsync(command);
	}
}

