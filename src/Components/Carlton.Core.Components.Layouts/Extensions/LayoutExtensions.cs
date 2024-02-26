using Carlton.Core.Components.Layouts.State.FullScreen;
using Carlton.Core.Components.Layouts.State.LayoutSettings;
using Carlton.Core.Components.Layouts.State.Modals;
using Carlton.Core.Components.Layouts.State.Theme;
using Carlton.Core.Components.Layouts.State.Toasts;
using Carlton.Core.Components.Layouts.State.Viewport;
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
    public static void AddCarltonLayout(this IServiceCollection services, Action<LayoutSettingsBuilder> act)
    {
        // Create a new LayoutSettingsBuilder instance and apply configuration
        var builder = new LayoutSettingsBuilder();
        act(builder);

        // Register layout state services and layout settings
        services.AddSingleton<IFullScreenState, FullScreenState>();
        services.AddSingleton<IThemeState, ThemeState>();
        services.AddSingleton<IModalState, ModalState>();
        services.AddSingleton<IToastState, ToastState>();
        services.AddSingleton<IViewportState, ViewportState>();
        services.AddSingleton<ILayoutSettings>(builder.Build());
    }
}
