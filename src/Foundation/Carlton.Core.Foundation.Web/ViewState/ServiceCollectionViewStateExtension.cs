using Microsoft.Extensions.DependencyInjection;
namespace Carlton.Core.Foundation.Web.ViewState;

public static class ServiceCollectionViewStateExtension
{
	public static void AddViewStateService<T>(this IServiceCollection services)
	{
		services.AddSingleton<IViewStateService<T>>(new ViewStateService<T>());
	}

	public static void AddViewStateService<T>(this IServiceCollection services, string key)
	{
		services.AddKeyedSingleton<IViewStateService<T>, ViewStateService<T>>(key);
	}
}
