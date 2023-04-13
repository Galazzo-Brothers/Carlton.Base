using Microsoft.Extensions.DependencyInjection;

namespace Carlton.Base.State;

public static class ContainerExtensions
{
    public static void AddCarltonState(this IServiceCollection services, params Assembly[] assemblies)
    {
        services.Scan(_ =>
            {
                _.FromAssemblies(assemblies)
                    .AddClasses(classes => classes.AssignableTo(typeof(ICarltonComponent<>)))
                    .AsImplementedInterfaces()
                    .WithSingletonLifetime();
                _.FromAssemblies(assemblies)
                    .AddClasses(classes => classes.AssignableTo(typeof(IViewModelRequest<>)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime();
                _.FromAssemblies(assemblies)
                    .AddClasses(classes => classes.AssignableTo(typeof(IComponentEventRequestFactory<,>)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime();
            });

        services.AddSingleton<ComponentEventRequestFactory>();
    }

}

