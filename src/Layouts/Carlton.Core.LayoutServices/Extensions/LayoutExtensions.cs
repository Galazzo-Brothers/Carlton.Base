using Carlton.Core.LayoutServices.FullScreen;
using Carlton.Core.LayoutServices.Modals;
using Carlton.Core.LayoutServices.Panel;
using Carlton.Core.LayoutServices.Theme;
using Carlton.Core.LayoutServices.Toasts;
using Carlton.Core.LayoutServices.Viewport;
using Carlton.Core.LayoutServices.ViewState;
using Microsoft.Extensions.DependencyInjection;
namespace Carlton.Core.LayoutServices.Extensions;

/// <summary>
/// Extension methods for configuring Carlton layout services.
/// </summary>
public static class LayoutExtensions
{
	/// <summary>
	/// Adds Carlton layout services to the specified <see cref="IServiceCollection"/>.
	/// </summary>
	/// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
	/// <param name="act">An action to configure layout settings.</param>
	public static void AddCarltonLayout(this IServiceCollection services,
		Action<LayoutOptions>? options = null)
	{
		//Layout Options
		var layoutOptions = new LayoutOptions();
		options?.Invoke(layoutOptions);

		// Register layout state services
		services.AddSingleton<IViewStateService, ViewStateService>();
		services.AddSingleton<IModalState, ModalState>();
		services.AddSingleton<IToastState, ToastState>();
		services.AddSingleton<IViewportState, ViewportState>();
		services.AddSingleton<IFullScreenState, FullScreenState>(_ => new FullScreenState(layoutOptions.IsFullScreen));
		services.AddSingleton<IThemeState, ThemeState>(_ => new ThemeState(layoutOptions.Theme));
		services.AddSingleton<IPanelState, PanelState>(_ => new PanelState(layoutOptions.ShowPanel));
	}
}
