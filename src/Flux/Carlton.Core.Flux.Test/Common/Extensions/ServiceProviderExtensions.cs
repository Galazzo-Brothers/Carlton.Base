namespace Carlton.Core.Components.Flux.Tests.Common.Extensions;

public static class ServiceProviderExtensions
{
    public static void SetupServiceProvider<T>(this IServiceProvider serviceProvider, object implementation)
    {
        serviceProvider.GetService(Arg.Is<Type>(typeof(T))).Returns(implementation);
    }
}
