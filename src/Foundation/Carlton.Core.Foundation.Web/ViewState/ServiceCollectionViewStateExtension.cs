using Microsoft.Extensions.DependencyInjection;
namespace Carlton.Core.Foundation.Web.ViewState;

public static class ServiceCollectionViewStateExtension
{
	public static void AddViewStateService<T>(this IServiceCollection services)
	{
		services.AddSingleton<IViewStateService<T>>(new ViewStateService<T>());
	}

	public static void AddViewStateService<T>(this IServiceCollection services, T viewState)
	{
		services.AddSingleton<IViewStateService<T>>(new ViewStateService<T>(viewState));
	}

	public static void AddViewStateService<T>(this IServiceCollection services, string key)
	{
		services.AddKeyedSingleton<IViewStateService<T>, ViewStateService<T>>(key);
	}

	public static void AddViewStateService<T>(this IServiceCollection services, T viewState, string key)
	{
		services.AddKeyedSingleton<IViewStateService<T>>(key, new ViewStateService<T>(viewState));
	}
}
