using Carlton.Core.Flux.Components;
using System.Reflection;
namespace Carlton.Core.Flux.Debug.Extensions;

internal class FluxTypes
{
	public IEnumerable<Type> ViewModelTypes { get; init; } = new List<Type>();

	public IEnumerable<Type> CommandTypes { get; init; } = new List<Type>();

	private FluxTypes() { }

	public static FluxTypes Create<TState>(IEnumerable<Type> ignoreTypes = null)
	{
		var vmTypes = new List<Type>();
		var commandTypes = new List<Type>();
		var assembly = Assembly.GetAssembly(typeof(TState));

		foreach (var type in assembly.GetTypes())
		{
			if (type.BaseType != null &&
				type.BaseType.IsGenericType &&
				type.BaseType.GetGenericTypeDefinition() == typeof(BaseConnectedComponent<>))
			{
				var genericType = type.BaseType?.GetGenericArguments().FirstOrDefault();
				if (genericType != null)
				{
					if (ignoreTypes == null || !ignoreTypes.Contains(genericType))
						vmTypes.Add(genericType);
				}
			}

			if (type.GetInterfaces().Any(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IFluxStateMutation<,>)))
			{
				var genericType = type.GetInterfaces().First(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IFluxStateMutation<,>)).GetGenericArguments()[1];
				if (genericType != null)
				{
					if (ignoreTypes == null || !ignoreTypes.Contains(genericType))
						commandTypes.Add(genericType);
				}
			}
		}

		return new FluxTypes
		{
			ViewModelTypes = vmTypes.ToArray(),
			CommandTypes = commandTypes.ToArray()
		};
	}
}