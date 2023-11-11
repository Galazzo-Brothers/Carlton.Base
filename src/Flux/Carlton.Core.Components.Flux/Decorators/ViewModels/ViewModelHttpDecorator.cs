using Carlton.Core.Components.Flux.Decorators.Base;
using System.Net.Http.Json;
using System.Text.Json;

namespace Carlton.Core.Components.Flux.Decorators.ViewModels;

public class ViewModelHttpDecorator<TState> : BaseHttpDecorator<TState>, IViewModelQueryDispatcher<TState>
{
    private readonly IViewModelQueryDispatcher<TState> _decorated;
    private readonly ILogger<ViewModelHttpDecorator<TState>> _logger;

    public ViewModelHttpDecorator(
        IViewModelQueryDispatcher<TState> decorated,
        HttpClient client,
        IMutableFluxState<TState> fluxState,
        ILogger<ViewModelHttpDecorator<TState>> logger) : base(client, fluxState)
        => (_decorated, _logger) = (decorated, logger);

    public async Task<TViewModel> Dispatch<TViewModel>(object sender, ViewModelQuery query, CancellationToken cancellationToken)
    {
        try
        {
            //Get RefreshPolicy Attribute
            var attributes = sender.GetType().GetCustomAttributes();
            var httpRefreshAttribute = attributes.OfType<ViewModelHttpRefreshAttribute>().FirstOrDefault();
            var requiresRefresh = GetRefreshPolicy(httpRefreshAttribute);
            var vmType = typeof(TViewModel).GetDisplayName();

            if (requiresRefresh)
            {
                //Log HttpRefresh Process
                Log.ViewModelHttpRefreshStarted(_logger, vmType);

                //Construct Http Refresh URL
                var urlParameterAttributes = attributes.OfType<HttpRefreshParameterAttribute>() ?? new List<HttpRefreshParameterAttribute>();
                var serverUrl = GetServerUrl(httpRefreshAttribute, urlParameterAttributes, sender);

                //Http Refresh ViewModel
                var viewModel = await _client.GetFromJsonAsync<TViewModel>(serverUrl, cancellationToken);

                //Update the StateStore
                await _fluxState.MutateState(viewModel);

                //Logging and Auditing 
                Log.ViewModelHttpRefreshCompleted(_logger, vmType);
            }
            else
            {
                Log.ViewModelHttpRefreshSkipped(_logger, vmType);
            }

            return await _decorated.Dispatch<TViewModel>(sender, query, cancellationToken);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains(LogEvents.InvalidRefreshUrlMsg))
        {
            //URL Construction Errors
            Log.ViewModelHttpUrlError(_logger, ex, typeof(TViewModel).GetDisplayName());
            throw ViewModelFluxException<TState, TViewModel>.HttpUrlError(query, ex);
        }
        catch (JsonException ex)
        {
            //Error Serializing JSON
            Log.ViewModelJsonError(_logger, ex, typeof(TViewModel).GetDisplayName());
            throw ViewModelFluxException<TState, TViewModel>.JsonError(query, ex);
        }
        catch (NotSupportedException ex) when (ex.Message.Contains("Serialization and deserialization"))
        {
            //Error Serializing JSON
            Log.ViewModelJsonError(_logger, ex, typeof(TViewModel).GetDisplayName());
            throw ViewModelFluxException<TState, TViewModel>.JsonError(query, ex);
        }
        catch (HttpRequestException ex)
        {
            //Http Exceptions
            Log.ViewModelHttpRefreshError(_logger, ex, typeof(TViewModel).GetDisplayName());
            throw ViewModelFluxException<TState, TViewModel>.HttpError(query, ex);
        }   
    }
}
