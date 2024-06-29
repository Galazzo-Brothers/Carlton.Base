using NSubstitute;
namespace Carlton.Core.Foundation.Tests;

public static class ServiceProviderExtensions
{
	public static void SetupServiceProvider<T>(this IServiceProvider serviceProvider, object implementation)
	{
		serviceProvider.GetService(Arg.Is(typeof(T))).Returns(implementation);
	}
}
