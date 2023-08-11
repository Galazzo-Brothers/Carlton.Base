using Carlton.Core.Components.Flux.Handlers;
using Carlton.Core.Components.Flux.State;
using Carlton.Core.InProcessMessaging.Commands;
using Carlton.Core.InProcessMessaging.Queries;

namespace Carlton.Core.Components.Flux;

public static class ContainerExtensions
{
    public static void AddCarltonFlux<TState>(this IServiceCollection services, params Assembly[] assemblies)
    {
        var stateEvents = new List<string>
        {
            "MenuItemSelected",
            "ParametersUpdated",
            "EventRecorded",
            "EventsCleared"
        };

        /*Connected Components*/
        services.Scan(scan => scan
            .FromApplicationDependencies()
            .AddClasses(classes => classes.AssignableTo(typeof(IConnectedComponent<>)))
            .AsImplementedInterfaces()
            .WithTransientLifetime());

        /*Command Dispatchers*/
        services.AddSingleton<ICommandDispatcher, CommandDispatcher>();
        //services.Decorate<ICommandDispatcher, CommandValidationDecorator>();
        //services.Decorate<ICommandDispatcher, CommandExceptionDecorator>();
        //services.Decorate<ICommandDispatcher, CommandHttpDecorator<TState>>();

        /*Query Dispatchers*/
        services.AddSingleton<IQueryDispatcher, QueryDispatcher>();
        //services.Decorate<IViewModelDispatcher, UtilityViewModelDecorator>();
        //services.Decorate<IViewModelDispatcher, ViewModelHttpDecorator<TState>>();
        //services.Decorate<IViewModelDispatcher, ViewModelJsDecorator<TState>>();

        /*Query Handlers*/
        RegisterViewModelHandlers<TState>(services);
        RegisterMutationCommandHandlers<TState>(services);

        services.Scan(_ => _
            .FromApplicationDependencies()
            .AddClasses(classes => classes.AssignableTo(typeof(IFluxStateMutation<,>)))
            .AsImplementedInterfaces()
            .WithTransientLifetime());

        /*State Mutations*/
        services.AddSingleton(new StateMutationEvents<TState>(stateEvents));
    }

    private static void RegisterMutationCommandHandlers<TState>(IServiceCollection services)
    {
        AppDomain.CurrentDomain.GetAssemblies()
                         .SelectMany(_ => _.DefinedTypes)
                         .Where(_ => _.IsAssignableTo(typeof(MutationCommand)))
                         .ToList().ForEach(_ =>
                         {
                             var interfaceType = typeof(ICommandHandler<,>).MakeGenericType(_, typeof(Unit));
                             var implementationType = typeof(MutationCommandHandler<,>).MakeGenericType(typeof(TState), _);
                             services.AddTransient(interfaceType, implementationType);
                         });
    }

    private static void RegisterViewModelHandlers<TState>(IServiceCollection services)
    {
        AppDomain.CurrentDomain.GetAssemblies()
               .SelectMany(_ => _.DefinedTypes)
               .Where(_ => _.Namespace != null && _.Namespace.Contains("Carlton"))
               .Where(_ => _.Name.EndsWith("ViewModel"))
               //  .Where(_ => !_.Name.Contains("Mutation"))
               .ToList().ForEach(_ =>
               {
                   var interfaceType = typeof(IQueryHandler<,>).MakeGenericType(typeof(ViewModelQuery), _);
                   var implementationType = typeof(BaseViewModelQueryHandler<,>).MakeGenericType(typeof(TState), _);
                   services.AddTransient(interfaceType, implementationType);
               });
    }
}


