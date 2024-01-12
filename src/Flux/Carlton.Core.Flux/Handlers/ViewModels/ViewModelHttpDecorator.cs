using System.Net;
using System.Net.Http.Json;
using Carlton.Core.Flux.Attributes;
using Carlton.Core.Flux.Exceptions;
using Carlton.Core.Flux.Handlers.Base;
using Carlton.Core.Flux.State;


namespace Carlton.Core.Flux.Handlers.ViewModels;

public class ViewModelHttpDecorator<TState> : BaseHttpDecorator<TState>, IViewModelQueryDispatcher<TState>
{
    private readonly IViewModelQueryDispatcher<TState> _decorated;
    private readonly ILogger<ViewModelHttpDecorator<TState>> _logger;

    public ViewModelHttpDecorator(
        IViewModelQueryDispatcher<TState> decorated,
        HttpClient client,
        TState state,
        ILogger<ViewModelHttpDecorator<TState>> logger) : base(client, state)
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
                //Update the context for logging and auditing
                context.MarkAsRequiresHttpRefresh();

                //Construct Http Refresh URL
                var urlParameterAttributes = attributes.OfType<HttpRefreshParameterAttribute>() ?? new List<HttpRefreshParameterAttribute>();
                var serverUrl = GetServerUrl(httpRefreshAttribute, urlParameterAttributes, sender);

                //Http Refresh ViewModel
                var viewModel = await _client.GetFromJsonAsync<TViewModel>(serverUrl, cancellationToken);
                context.MarkAsHttpCallMade(serverUrl, HttpStatusCode.OK, viewModel);

                //Update the StateStore
                var command = new ViewModelRemoteRefreshCommand<TViewModel>(viewModel);
                var commandContext = new MutationCommandContext<ViewModelRemoteRefreshCommand<TViewModel>>(command);
               // await _state.ApplyMutationCommand(commandContext);
                context.MarkAsStateModifiedByHttpRefresh();
            }

            return await _decorated.Dispatch(sender, context, cancellationToken);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains(LogEvents.InvalidRefreshUrlMsg))
        {
            //URL Construction Errors
            _logger.ViewModelHttpUrlError(ex, context.ViewModelType);
            throw ViewModelFluxException<TState, TViewModel>.HttpUrlError(context, ex);
        }
        catch (JsonException ex)
        {
            //Error Serializing JSON
            _logger.ViewModelHttpResponseJsonError(ex, context.ViewModelType);
            throw ViewModelFluxException<TState, TViewModel>.JsonError(context, ex);
        }
        catch (NotSupportedException ex) when (ex.Message.Contains("Serialization and deserialization"))
        {
            //Error Serializing JSON
            _logger.ViewModelHttpResponseJsonError(ex, context.ViewModelType);
            throw ViewModelFluxException<TState, TViewModel>.JsonError(context, ex);
        }
        catch (HttpRequestException ex)
        {
            //Http Exceptions
            _logger.ViewModelHttpRequestError(ex, context.ViewModelType);
            throw ViewModelFluxException<TState, TViewModel>.HttpError(context, ex);
        }
    }
}
