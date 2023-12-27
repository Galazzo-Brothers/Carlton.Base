using Carlton.Core.Components.Layouts.State;
using Microsoft.Extensions.DependencyInjection;

namespace Carlton.Core.Components.Layouts.Extensions;

public static class Extensions
{
    public static void AddCarltonLayout(this IServiceCollection services)
    {
        services.AddSingleton<ILayoutState, LayoutState>();
    }
}
