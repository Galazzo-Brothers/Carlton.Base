using Carlton.Core.Flux.Attributes;
using Carlton.Core.Flux.Exceptions;
using Carlton.Core.Flux.Handlers.Base;
using Carlton.Core.Flux.State;
using System.Net.Http.Json;

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

    public async Task<TViewModel> Dispatch<TViewModel>(object sender, ViewModelQueryContext<TViewModel> context, CancellationToken cancellationToken)
    {
        try
        {
            //Get RefreshPolicy Attribute
            var attributes = sender.GetType().GetCustomAttributes();
            var httpRefreshAttribute = attributes.OfType<ViewModelHttpRefreshAttribute>().FirstOrDefault();
            var requiresRefresh = GetRefreshPolicy(httpRefreshAttribute);

            if (requiresRefresh)
            {
                //Construct Http Refresh URL
                var urlParameterAttributes = attributes.OfType<HttpRefreshParameterAttribute>() ?? new List<HttpRefreshParameterAttribute>();
                var serverUrl = GetServerUrl(httpRefreshAttribute, urlParameterAttributes, sender);

                //Http Refresh ViewModel
                var viewModel = await _client.GetFromJsonAsync<TViewModel>(serverUrl, cancellationToken);

                //Update the StateStore
                var command = new ViewModelRemoteRefreshCommand<TViewModel>(viewModel);
                await _fluxState.MutateState(command);

                //Logging and Auditing 
                context.MarkAsHttpCallMade(serverUrl, viewModel);
                _logger.ViewModelHttpRefreshCompleted(context.ViewModelType);
            }
            else
            {
                _logger.ViewModelHttpRefreshSkipped(context.ViewModelType);
            }

            return await _decorated.Dispatch(sender, context, cancellationToken);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains(LogEvents.InvalidRefreshUrlMsg))
        {
            //URL Construction Errors
            context.MarkAsErrored();
            _logger.ViewModelHttpUrlError(ex, context.ViewModelType);
            throw ViewModelFluxException<TState, TViewModel>.HttpUrlError(context, ex);
        }
        catch (JsonException ex)
        {
            //Error Serializing JSON
            context.MarkAsErrored();
            _logger.ViewModelHttpResponseJsonError(ex, context.ViewModelType);
            throw ViewModelFluxException<TState, TViewModel>.JsonError(context, ex);
        }
        catch (NotSupportedException ex) when (ex.Message.Contains("Serialization and deserialization"))
        {
            //Error Serializing JSON
            context.MarkAsErrored();
            _logger.ViewModelHttpResponseJsonError(ex, context.ViewModelType);
            throw ViewModelFluxException<TState, TViewModel>.JsonError(context, ex);
        }
        catch (HttpRequestException ex)
        {
            //Http Exceptions
            context.MarkAsErrored();
            _logger.ViewModelHttpRequestError(ex, context.ViewModelType);
            throw ViewModelFluxException<TState, TViewModel>.HttpError(context, ex);
        }
    }
}
