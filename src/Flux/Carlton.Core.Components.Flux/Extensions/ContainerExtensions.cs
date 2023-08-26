using Carlton.Core.Components.Flux.Contracts;
using Carlton.Core.Components.Flux.Decorators.Commands;
using Carlton.Core.Components.Flux.Decorators.Queries;
using Carlton.Core.Components.Flux.Decorators.ViewModels;
using Carlton.Core.Components.Flux.Dispatchers;
using Carlton.Core.Components.Flux.ExceptionHandling;
using Carlton.Core.Components.Flux.Handlers;
using Carlton.Core.Components.Flux.State;

namespace Carlton.Core.Components.Flux;

public static class ContainerExtensions
{
    public static void AddCarltonFlux<TState>(this IServiceCollection services, TState state)
        where TState : class
    {
        /*Flux State*/
        RegisterFluxState(services, state);

        /*Connected Components*/
        RegisterFluxConnectedComponents(services);

        /*Dispatchers*/
        RegisterFluxDispatchers<TState>(services);

        /*Handlers*/
        RegisterFluxHandlers<TState>(services);

        /*State Mutations*/
        RegisterFluxStateMutations(services);

        /*Validators*/
        RegisterValidators(services);

        /*Exception Handling*/
        RegisterExceptionHandling(services);

    }

    private static void RegisterFluxState<TState>(IServiceCollection services, TState state)
        where TState : class
    {
        /*LabState*/
        services.AddSingleton(state);

        services.AddSingleton<IMutableFluxState<TState>, FluxState<TState>>();
        services.AddSingleton<IFluxState<TState>>(_ => _.GetService<IMutableFluxState<TState>>());
        services.AddSingleton<IFluxStateObserver<TState>>(_ => _.GetService<IFluxState<TState>>());
        services.AddSingleton<MutationResolver<TState>>();
    }

    private static void RegisterFluxDispatchers<TState>(IServiceCollection services)
    {
        /*ViewModel Dispatchers*/
        services.AddSingleton<IViewModelQueryDispatcher<TState>, ViewModelQueryDispatcher<TState>>();
        services.Decorate<IViewModelQueryDispatcher<TState>, ViewModelValidationDecorator<TState>>();
        services.Decorate<IViewModelQueryDispatcher<TState>, ViewModelExceptionDecorator<TState>>();
        //services.Decorate<IViewModelQueryDispatcher<TState>, ViewModelHttpDecorator<TState>>();
        services.Decorate<IViewModelQueryDispatcher<TState>, ViewModelJsDecorator<TState>>();

        /*Mutation Dispatchers*/
        services.AddSingleton<IMutationCommandDispatcher<TState>, MutationCommandDispatcher<TState>>();
        services.Decorate<IMutationCommandDispatcher<TState>, MutationValidationDecorator<TState>>();
        services.Decorate<IMutationCommandDispatcher<TState>, MutationExceptionDecorator<TState>>();
    }

    private static void RegisterFluxConnectedComponents(IServiceCollection services)
    {
        services.Scan(scan => scan
                    .FromApplicationDependencies()
                    .AddClasses(classes => classes.AssignableTo(typeof(IConnectedComponent<>)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());
    }

    private static void RegisterFluxHandlers<TState>(IServiceCollection services)
    {
        bool interfacePredicate(Type _) => _.IsGenericType && _.GetGenericTypeDefinition() == typeof(IConnectedComponent<>);
        bool baseConnComPredicate(Type _) => _.IsGenericType && _.GetGenericTypeDefinition() == typeof(BaseConnectedComponent<>);
        bool isCommandPredicate(Type _) => _.IsAssignableTo(typeof(MutationCommand));
        bool isConnComPredicate(Type _) => _.GetInterfaces().Any(interfacePredicate) && !baseConnComPredicate(_);

        AppDomain.CurrentDomain.GetAssemblies()
                        .SelectMany(_ => _.DefinedTypes)
                        .Where(_ => _.IsClass && !_.IsAbstract)
                        .Where(_ => isCommandPredicate(_) || isConnComPredicate(_))
                        .ToList().ForEach(_ =>
                        {
                            var isMutationCommand = isCommandPredicate(_);
                            var isConnectedComponent = isConnComPredicate(_);

                            if (isConnectedComponent)
                            {
                                //Register ViewModel Queries
                                var vmType = _.GetInterfaces().First(interfacePredicate).GetGenericArguments()[0];
                                var interfaceType = typeof(IViewModelQueryHandler<,>).MakeGenericType(typeof(TState), vmType);
                                var implementationType = typeof(ViewModelQueryHandler<,>).MakeGenericType(typeof(TState), vmType);
                                services.AddTransient(interfaceType, implementationType);
                            }
                            else if (isMutationCommand)
                            {
                                //Register Mutation Commands
                                var interfaceType = typeof(IMutationCommandHandler<,>).MakeGenericType(typeof(TState), _);
                                var implementationType = typeof(MutationCommandHandler<,>).MakeGenericType(typeof(TState), _);
                                services.AddTransient(interfaceType, implementationType);
                            }
                        });
    }

    private static void RegisterFluxStateMutations(IServiceCollection services)
    {
        services.Scan(_ => _
            .FromApplicationDependencies()
            .AddClasses(classes => classes.AssignableTo(typeof(IFluxStateMutation<,>)))
            .AsImplementedInterfaces()
            .WithTransientLifetime());
    }

    private static void RegisterValidators(IServiceCollection services)
    {
        services.Scan(_ =>
            _.FromApplicationDependencies()
            .AddClasses(classes => classes.AssignableTo(typeof(IValidator<>)))
            .AsImplementedInterfaces()
            .WithTransientLifetime());
    }

    private static void RegisterExceptionHandling(IServiceCollection services)
    {
        services.AddSingleton<IExceptionDisplayService, FluxExceptionDisplayService>();
        services.AddSingleton<IComponentExceptionLoggingService, ComponentExceptionLoggingService>();
    }
}


