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
        services.AddTransient<HttpClient>();
        services.AddSingleton<IViewModelDispatcher, ViewModelDispatcher>();
        services.Decorate<IViewModelDispatcher, UtilityViewModelDecorator>();
        services.Decorate<IViewModelDispatcher, ViewModelHttpDecorator>();

        services.AddSingleton<ICommandDispatcher, CommandDispatcher>();
        services.Decorate<ICommandDispatcher, UtilityCommandDecorator>();
        services.Decorate<ICommandDispatcher, CommandHttpDecorator>();


        services.Scan(selector =>
        {
            selector.FromAssemblies(assemblies)
                    .AddClasses(classes => classes.AssignableTo(typeof(IViewModelHandler<>)))
                    .AsImplementedInterfaces()
                    .WithSingletonLifetime();
        });
        services.Scan(selector =>
        {
            selector.FromAssemblies(assemblies)
                    .AddClasses(filter => filter.AssignableTo(typeof(ICommandHandler<>)))
                    .AsImplementedInterfaces()
                    .WithSingletonLifetime();
        });
    }
}

