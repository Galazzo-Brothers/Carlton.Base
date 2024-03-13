﻿using Carlton.Core.Flux.Internals.Dispatchers.Mutations;
using Carlton.Core.Flux.Internals.Dispatchers.Mutations.Decorators;
using Carlton.Core.Flux.Internals.Dispatchers.ViewModels;
using Carlton.Core.Flux.Internals.Dispatchers.ViewModels.Decorators;
using Carlton.Core.Flux.Internals.State;
using Carlton.Core.Utilities.Logging;

namespace Carlton.Core.Flux.Extensions;

/// <summary>
/// Extension methods for registering Carlton Flux services.
/// </summary>
public static class ServiceCollectionExtensions
{
	/// <summary>
	/// Registers Carlton Flux services with the specified state and options.
	/// </summary>
	/// <typeparam name="TState">The type of state for the Flux application.</typeparam>
	/// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
	/// <param name="state">The initial state for the Flux application.</param>
	/// <param name="options">An action to configure the Flux options.</param>
	/// <remarks>
	/// Use this method to register Carlton Flux services at startup, including state management and dependencies.
	/// </remarks>
	public static void AddCarltonFlux<TState>(this IServiceCollection services, TState state, Action<FluxOptions>? options = null)
	{
		//Flux Options
		var fluxOptions = new FluxOptions();
		options?.Invoke(fluxOptions);

		/*Flux State*/
		RegisterCrossCuttingDependencies(services);
		RegisterFluxState(services, state);
		RegisterFluxDependencies<TState>(services, fluxOptions);
	}

	private static void RegisterCrossCuttingDependencies(IServiceCollection services)
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

	private static void RegisterFluxDependencies<TState>(IServiceCollection services, FluxOptions fluxOptions)
	{
		/*Flux Connected Components*/
		RegisterFluxConnectedComponents(services);

		/*Dispatchers*/
		RegisterFluxDispatchers<TState>(services, fluxOptions);

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
		services.AddSingleton<IMutableFluxState<TState>>(sp => new FluxState<TState>(state, sp.GetService<IServiceProvider>()));
		services.AddSingleton<IFluxState<TState>>(sp => sp.GetService<IMutableFluxState<TState>>());
		services.AddSingleton<IFluxStateObserver<TState>>(sp => sp.GetService<IFluxState<TState>>());
	}

	private static void RegisterFluxDispatchers<TState>(IServiceCollection services, FluxOptions fluxOptions)
	{
		/*ViewModel Dispatchers*/
		RegisterViewModelDecorators<TState>(services, fluxOptions);

		/*Mutation Dispatchers*/
		RegisterCommandDecorators<TState>(services, fluxOptions);
	}

	private static void RegisterCommandDecorators<TState>(IServiceCollection services, FluxOptions fluxOptions)
	{
		services.AddSingleton<IMutationCommandDispatcher<TState>, MutationCommandDispatcher<TState>>();

		if (fluxOptions.AddHttpInterception)
			services.Decorate<IMutationCommandDispatcher<TState>, MutationHttpDecorator<TState>>();

		if (fluxOptions.AddLocalStorage)
			services.Decorate<IMutationCommandDispatcher<TState>, MutationLocalStorageDecorator<TState>>();

		services.Decorate<IMutationCommandDispatcher<TState>, MutationValidationDecorator<TState>>();
		services.Decorate<IMutationCommandDispatcher<TState>, MutationExceptionDecorator<TState>>();
	}

	private static void RegisterViewModelDecorators<TState>(IServiceCollection services, FluxOptions fluxOptions)
	{
		services.AddSingleton<IViewModelQueryDispatcher<TState>, ViewModelQueryDispatcher<TState>>();

		if (fluxOptions.AddHttpInterception)
			services.Decorate<IViewModelQueryDispatcher<TState>, ViewModelHttpDecorator<TState>>();

		services.Decorate<IViewModelQueryDispatcher<TState>, ViewModelValidationDecorator<TState>>();
		services.Decorate<IViewModelQueryDispatcher<TState>, ViewModelExceptionDecorator<TState>>();
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
		  .AddClasses(classes => classes.AssignableTo(typeof(IViewModelProjectionMapper<TState>)))
		  .AsImplementedInterfaces()
		  .WithSingletonLifetime());
	}
}


