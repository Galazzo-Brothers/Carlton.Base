namespace Carlton.Base.State;

public static class ContainerExtensions
{
    public static void AddCarltonState(this IServiceCollection services, params Assembly[] assemblies)
    {
        services.Scan(_ =>
            {
                _.FromAssemblies(assemblies)
                    .AddClasses(classes => classes.AssignableTo(typeof(IDataComponent<>)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime();

            });
    }
}

