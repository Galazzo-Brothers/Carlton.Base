using Carlton.Core.Components.Layouts.State.FullScreen;
using Carlton.Core.Components.Layouts.State.Modals;
using Carlton.Core.Components.Layouts.State.Theme;
using Carlton.Core.Components.Layouts.State.Toasts;
using Microsoft.Extensions.DependencyInjection;

namespace Carlton.Core.Components.Layouts.Extensions;

public static class Extensions
{
    public static void AddCarltonLayout(this IServiceCollection services)
    {
        services.AddSingleton<IFullScreenState, FullScreenState>();
        services.AddSingleton<IThemeState, ThemeState>();
        services.AddSingleton<IModalState, ModalState>();
        services.AddSingleton<IToastState, ToastState>();
    }
}
