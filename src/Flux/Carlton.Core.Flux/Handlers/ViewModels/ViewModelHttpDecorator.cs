using Carlton.Core.Flux.Attributes;
using Carlton.Core.Flux.Contracts;
using Carlton.Core.Flux.Exceptions;
using Carlton.Core.Flux.Handlers.Base;
using Carlton.Core.Flux.Logging;
using Carlton.Core.Flux.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace Carlton.Core.Flux.Handlers.ViewModels;

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
                _logger.ViewModelHttpRefreshStarted(vmType);

                //Construct Http Refresh URL
                var urlParameterAttributes = attributes.OfType<HttpRefreshParameterAttribute>() ?? new List<HttpRefreshParameterAttribute>();
                var serverUrl = GetServerUrl(httpRefreshAttribute, urlParameterAttributes, sender);

                //Http Refresh ViewModel
                var viewModel = await _client.GetFromJsonAsync<TViewModel>(serverUrl, cancellationToken);

                //Update the StateStore
                await _fluxState.MutateState(viewModel);

                //Logging and Auditing 
                _logger.ViewModelHttpRefreshCompleted(vmType);
            }
            else
            {
                _logger.ViewModelHttpRefreshSkipped(vmType);
            }

            return await _decorated.Dispatch<TViewModel>(sender, query, cancellationToken);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains(LogEvents.InvalidRefreshUrlMsg))
        {
            //URL Construction Errors
            _logger.ViewModelHttpUrlError(ex, typeof(TViewModel).GetDisplayName());
            throw ViewModelFluxException<TState, TViewModel>.HttpUrlError(query, ex);
        }
        catch (JsonException ex)
        {
            //Error Serializing JSON
            _logger.ViewModelJsonError(ex, typeof(TViewModel).GetDisplayName());
            throw ViewModelFluxException<TState, TViewModel>.JsonError(query, ex);
        }
        catch (NotSupportedException ex) when (ex.Message.Contains("Serialization and deserialization"))
        {
            //Error Serializing JSON
            _logger.ViewModelJsonError(ex, typeof(TViewModel).GetDisplayName());
            throw ViewModelFluxException<TState, TViewModel>.JsonError(query, ex);
        }
        catch (HttpRequestException ex)
        {
            //Http Exceptions
            _logger.ViewModelHttpRefreshError(ex, typeof(TViewModel).GetDisplayName());
            throw ViewModelFluxException<TState, TViewModel>.HttpError(query, ex);
        }
    }
}
