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
            });


        services.AddSingleton<IComponentEventRequestFactory, ComponentEventRequestFactory>(_ => new ComponentEventRequestFactory(GetEventRequestTypes(assemblies)));
    }

    private static IEnumerable<Type> GetEventRequestTypes(Assembly[] assemblies)
    {
        return  assemblies.SelectMany(t => t.ExportedTypes)
                  .Where(t => t.GetInterfaces()
                  .Any(i => i.IsGenericType && i.GetGenericTypeDefinition().Equals(typeof(IComponentEventRequest<,>))))
                  .ToList();

    }

}

