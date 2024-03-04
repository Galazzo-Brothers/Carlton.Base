using Carlton.Core.Flux.Dispatchers.Mutations;
using Carlton.Core.Flux.Dispatchers.Mutations.Decorators;
using Carlton.Core.Flux.Dispatchers.ViewModels;
using Carlton.Core.Flux.Dispatchers.ViewModels.Decorators;
using Carlton.Core.Flux.Handlers;
using Carlton.Core.Flux.State;
using Carlton.Core.Utilities.Logging;

namespace Carlton.Core.Flux.Extensions;

public static class ServiceCollectionExtensions
{
    private static bool AddedLocalStorage = false;

    public static void AddCarltonFlux<TState>(this IServiceCollection services, TState state)
    {
        if (!AddedLocalStorage)
            RegisterFluxDependencies(services);

        RegisterStateSpecificFluxDependencies(services, state);

        AddedLocalStorage = true;
    }

    private static void RegisterFluxDependencies(IServiceCollection services)
    {
        /*Register Logging*/
        RegisterLogging(services);
    }

    private static void RegisterLogging(IServiceCollection services)
    {
        var logger = new MemoryLogger();
        services.AddSingleton(logger);
        services.AddLogging(b =>
        {
            b.AddProvider(new MemoryLoggerProvider(logger));
        });

        services.AddSingleton<ILogger, MemoryLogger>();
    }

    private static void RegisterStateSpecificFluxDependencies<TState>(IServiceCollection services, TState state) 
    {
        /*Flux Connected Components*/
        RegisterFluxConnectedComponents(services);

        /*Flux State*/
        RegisterFluxState<TState>(services, state);

        /*Dispatchers*/
        RegisterFluxDispatchers<TState>(services);

        /*Handlers*/
        RegisterFluxHandlers<TState>(services);

        /*State Mutations*/
        RegisterFluxStateMutations<TState>(services);

        /*ViewModel Projection Mapper*/
        RegisterViewModelProjectionMapper<TState>(services);
    }

    private static void RegisterFluxConnectedComponents(IServiceCollection services)
    {
        services.Scan(scan => scan
                    .FromApplicationDependencies()
                    .AddClasses(classes => classes.AssignableTo(typeof(IConnectedComponent<>)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());
    }


    private static void RegisterFluxState<TState>(IServiceCollection services, TState state)
    {
        //Register State Wrappers
        services.AddSingleton<IMutableFluxState<TState>>(_ => new FluxState<TState>(state, new MutationResolver<TState>(_)));
        services.AddSingleton<IFluxState<TState>>(_ => _.GetService<IMutableFluxState<TState>>());
        services.AddSingleton<IFluxStateObserver<TState>>(_ => _.GetService<IFluxState<TState>>());
    }

    private static void RegisterFluxDispatchers<TState>(IServiceCollection services)
    {
        /*ViewModel Dispatchers*/
        services.AddSingleton<IViewModelQueryDispatcher<TState>, ViewModelQueryDispatcher<TState>>();
        //services.Decorate<IViewModelQueryDispatcher<TState>, ViewModelHttpDecorator<TState>>();
        services.Decorate<IViewModelQueryDispatcher<TState>, ViewModelValidationDecorator<TState>>();
        services.Decorate<IViewModelQueryDispatcher<TState>, ViewModelExceptionDecorator<TState>>();

        /*Mutation Dispatchers*/
        services.AddSingleton<IMutationCommandDispatcher<TState>, MutationCommandDispatcher<TState>>();
        services.Decorate<IMutationCommandDispatcher<TState>, MutationValidationDecorator<TState>>();
        // services.Decorate<IMutationCommandDispatcher<TState>, MutationHttpDecorator<TState>>();
        services.Decorate<IMutationCommandDispatcher<TState>, MutationExceptionDecorator<TState>>();
    }

    private static void RegisterFluxHandlers<TState>(IServiceCollection services)
    {
        services.AddSingleton<IMutationCommandHandler<TState>, MutationCommandHandler<TState>>();
        services.AddSingleton<IViewModelQueryHandler<TState>, ViewModelQueryHandler<TState>>();
    }

    private static void RegisterFluxStateMutations<TState>(IServiceCollection services)
    {
        services.Scan(_ => _
            .FromApplicationDependencies()
            .AddClasses(classes => classes.AssignableTo(typeof(IFluxStateMutation<,>)))
            .AsImplementedInterfaces()
            .WithSingletonLifetime());
    }

    private static void RegisterViewModelProjectionMapper<TState>(IServiceCollection services)
    {
        services.Scan(scan => scan
          .FromApplicationDependencies()
          .AddClasses(classes => classes.AssignableTo(typeof(IViewModelMapper<TState>)))
          .AsImplementedInterfaces()
          .WithSingletonLifetime());
    }
}


