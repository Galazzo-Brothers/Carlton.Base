using FluentValidation;

namespace Carlton.Base.State;

public static class ContainerExtensions
{
    public static void AddCarltonState<TState>(this IServiceCollection services, params Assembly[] assemblies)
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
        services.Decorate<IViewModelDispatcher, ViewModelHttpDecorator<TState>>();
        services.Decorate<IViewModelDispatcher, ViewModelJsDecorator<TState>>();

        services.AddSingleton<ICommandDispatcher, CommandDispatcher>();
        services.Decorate<ICommandDispatcher, CommandExceptionDecorator>();
        services.Decorate<ICommandDispatcher, CommandHttpDecorator<TState>>();


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

        assemblies.SelectMany(_ => _.DefinedTypes).Where(_ => _.Name.Contains("ViewModel"))
            .ToList()    
            .ForEach(_ =>
                {
                    var serviceType = typeof(IViewModelHandler<>).MakeGenericType(_);
                    var implementationType = typeof(ViewModelHandler<>).MakeGenericType(_);
                    services.AddSingleton(serviceType ,_ => ActivatorUtilities.CreateInstance(_, implementationType));
                });

        assemblies.SelectMany(_ => _.DefinedTypes).Where(_ => _.Name.Contains("Command"))//  && _.GetInterfaces().Contains(typeof(ICommand))); ;
           .ToList()
           .ForEach(_ =>
           {
               var serviceType = typeof(ICommandHandler<>).MakeGenericType(_);
               var implementationType = typeof(CommandHandler<>).MakeGenericType(_);
               services.AddSingleton(serviceType, _ => ActivatorUtilities.CreateInstance(_, implementationType));
           });
    }
}

