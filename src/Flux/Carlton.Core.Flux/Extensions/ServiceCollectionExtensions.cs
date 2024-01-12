using Carlton.Core.Flux.Dispatchers;
using Carlton.Core.Flux.Exceptions.ExceptionHandling;
using Carlton.Core.Flux.Handlers.Mutations;
using Carlton.Core.Flux.Handlers.ViewModels;
using Carlton.Core.Flux.State;
using Carlton.Core.Utilities.Logging;
using MapsterMapper;

namespace Carlton.Core.Flux.Extensions;

public static class ServiceCollectionExtensions
{
    private static bool AddedLocalStorage = false;

    public static void AddCarltonFlux<TState>(
        this IServiceCollection services,
        TState state)
        where TState : class
    {
        if (!AddedLocalStorage)
            RegisterFluxDependencies(services);

        RegisterStateSpecificFluxDependencies<TState>(services);

        services.AddSingleton(state);

        AddedLocalStorage = true;
    }

    private static void RegisterStateSpecificFluxDependencies<TState>(IServiceCollection services) where TState : class
    {
        /*Flux State*/
        RegisterFluxState<TState>(services);

        /*Dispatchers*/
        RegisterFluxDispatchers<TState>(services);

        /*Handlers*/
        RegisterFluxHandlers<TState>(services);

        /*State Mutations*/
        RegisterFluxStateMutations<TState>(services);
    }

    private static void RegisterFluxDependencies(IServiceCollection services)
    {
        /*Register Logging*/
        RegisterLogging(services);

        /*Mapster*/
        RegisterMapster(services);

        /*Validators*/
        RegisterValidators(services);

        /*Exception Handling*/
        RegisterExceptionHandling(services);

        /*Connected Components*/
        RegisterFluxConnectedComponents(services);
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

    private static void RegisterMapster(IServiceCollection services)
    {
        services.Scan(scan => scan
                  .FromApplicationDependencies()
                  .AddClasses(classes => classes.AssignableTo(typeof(IRegister)))
                  .AsImplementedInterfaces()
                  .WithSingletonLifetime());

        var config = new TypeAdapterConfig();
        services.AddSingleton(config);
        services.AddSingleton<IMapper, ServiceMapper>();
    }

    private static void RegisterFluxState<TState>(IServiceCollection services)
        where TState : class
    {
        var observable = new FluxStateObservable<TState>();
        services.AddSingleton<IFluxStateObserver<TState>>(observable);
        services.AddSingleton<IFluxStateObservable<TState>>(observable);
        services.AddSingleton<MutationResolver<TState>>();
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
        services.AddSingleton<IMutationCommandHandler<TState>, MutationCommandHandler<TState>>();
        services.AddSingleton<IViewModelQueryHandler<TState>, ViewModelQueryHandler<TState>>();
    }

    private static void RegisterFluxStateMutations<TState>(IServiceCollection services)
    {
        services.AddSingleton<IMutationResolver<TState>, MutationResolver<TState>>();

        services.Scan(_ => _
            .FromApplicationDependencies()
            .AddClasses(classes => classes.AssignableTo(typeof(IFluxStateMutation<,>)))
            .AsImplementedInterfaces()
            .WithSingletonLifetime());
    }

    private static void RegisterValidators(IServiceCollection services)
    {
        services.Scan(_ =>
            _.FromApplicationDependencies()
            .AddClasses(classes => classes.AssignableTo(typeof(IValidator<>)))
            .AsImplementedInterfaces()
            .WithSingletonLifetime());
    }

    private static void RegisterExceptionHandling(IServiceCollection services)
    {
        services.AddSingleton<IFluxExceptionDisplayService, FluxExceptionDisplayService>();
        services.AddSingleton<IComponentExceptionLoggingService, ComponentExceptionLoggingService>();
    }
}


