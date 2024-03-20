using Carlton.Core.Components.Layouts.FullScreen;
using Carlton.Core.Components.Layouts.Modals;
using Carlton.Core.Components.Layouts.Panel;
using Carlton.Core.Components.Layouts.Theme;
using Carlton.Core.Components.Layouts.Toasts;
using Carlton.Core.Components.Layouts.Viewport;
using Microsoft.Extensions.DependencyInjection;
namespace Carlton.Core.Components.Layouts.Extensions;

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
        services.AddSingleton<IModalState, ModalState>();
        services.AddSingleton<IToastState, ToastState>();
        services.AddSingleton<IViewportState, ViewportState>();
        services.AddSingleton<IFullScreenState, FullScreenState>(_ => new FullScreenState(layoutOptions.IsFullScreen));
        services.AddSingleton<IThemeState, ThemeState>(_ => new ThemeState(layoutOptions.Theme));
        services.AddSingleton<IPanelState, PanelState>(_ => new PanelState(layoutOptions.ShowPanel));
    }
}
