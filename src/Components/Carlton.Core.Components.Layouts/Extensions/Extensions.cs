using Carlton.Core.Components.Layouts.State.FullScreen;
using Carlton.Core.Components.Layouts.State.LayoutSettings;
using Carlton.Core.Components.Layouts.State.Modals;
using Carlton.Core.Components.Layouts.State.Theme;
using Carlton.Core.Components.Layouts.State.Toasts;
using Carlton.Core.Components.Layouts.State.Viewport;
using Microsoft.Extensions.DependencyInjection;

namespace Carlton.Core.Components.Layouts.Extensions;

public static class Extensions
{
    public static void AddCarltonLayout(this IServiceCollection services, Action<LayoutSettingsBuilder> act)
    {
        var builder = new LayoutSettingsBuilder();
        act(builder);

        services.AddSingleton<IFullScreenState, FullScreenState>();
        services.AddSingleton<IThemeState, ThemeState>();
        services.AddSingleton<IModalState, ModalState>();
        services.AddSingleton<IToastState, ToastState>();
        services.AddSingleton<IViewportState, ViewportState>();
        services.AddSingleton<ILayoutSettings>(builder.Build());
    }
}
