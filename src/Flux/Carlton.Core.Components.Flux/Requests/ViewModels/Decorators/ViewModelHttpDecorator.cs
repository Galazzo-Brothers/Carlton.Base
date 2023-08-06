namespace Carlton.Core.Components.Flux;

public class ViewModelHttpDecorator<TState> : IViewModelDispatcher
{
    private readonly IViewModelDispatcher _decorated;
    private readonly HttpClient _client;
    private readonly ILogger<ViewModelHttpDecorator<TState>> _logger;

    public ViewModelHttpDecorator(IViewModelDispatcher decorated, HttpClient client, ILogger<ViewModelHttpDecorator<TState>> logger)
        => (_decorated, _client, _logger) = (decorated, client, logger);

    public async Task<TViewModel> Dispatch<TViewModel>(ViewModelRequest<TViewModel> request, CancellationToken cancellationToken)
    {
        //Get RefreshPolicy Attribute
        var attributes = request.Sender.GetType().GetCustomAttributes();
        var refreshPolicyAttribute = attributes.OfType<ViewModelEndpointRefreshPolicyAttribute>().FirstOrDefault();
        var requiresRefresh = GetRefreshPolicy(refreshPolicyAttribute);

        if(requiresRefresh)
        {
            //Log HttpRefresh Process
            Log.ViewModelRequestHttpRefreshStarting(_logger, request.DisplayName, request);

            //Construct Http Refresh URL
            var urlAttribute = attributes.OfType<ViewModelEndpointAttribute>().FirstOrDefault();
            var urlParameterAttributes = attributes.OfType<ViewModelEndpointParameterAttribute>() ?? new List<ViewModelEndpointParameterAttribute>();
            var serverUrl = GetServerUrlWrapperCall(request, urlAttribute, urlParameterAttributes, request.Sender);

            //Update State with Http Refresh
            await RefreshViewModel(request, serverUrl, cancellationToken);

            //Logging and Auditing 
            refreshPolicyAttribute.InitialRequestOccurred = true;
            request.MarkAsServerCalled(serverUrl);
            Log.ViewModelRequestHttpRefreshCompleted(_logger, request.DisplayName, request);
        }
        else
        {
            Log.ViewModelRequestSkippingHttpRefresh(_logger, request.DisplayName, request);
        }

        return await _decorated.Dispatch(request, cancellationToken);
    }

    private async Task RefreshViewModel<TViewModel>(ViewModelRequest<TViewModel> request, string serverUrl, CancellationToken cancellationToken)
    {
        try
        {
            var viewModel = await _client.GetFromJsonAsync<TViewModel>(serverUrl, cancellationToken);
            viewModel.Adapt((TState) request.State);
        }
        catch(HttpRequestException ex)
        {
            request.MarkErrored(LogEvents.ViewModelRequest_HttpRefresh_Http_Error);
            Log.ViewModelRequestHttpRefreshError(_logger, ex, request.DisplayName, request);
            throw;
        }
        catch(Exception ex)
        {
            request.MarkErrored(LogEvents.ViewModelRequest_HttpRefresh_Mapping_Error);
            Log.ViewModelRequestHttpRefreshMappingError(_logger, ex, request.DisplayName, request);
            throw;
        }
    }

    private static bool GetRefreshPolicy(ViewModelEndpointRefreshPolicyAttribute attribute)
    {
        return attribute?.DataEndpointRefreshPolicy switch
        {
            DataEndpointRefreshPolicy.Never => false,
            DataEndpointRefreshPolicy.Always => true,
            DataEndpointRefreshPolicy.InitOnly => attribute.InitialRequestOccurred,
            _ => false
        };
    }

    private string GetServerUrlWrapperCall<TViewModel>(
        ViewModelRequest<TViewModel> request,
        ViewModelEndpointAttribute endpointAttribute,
        IEnumerable<ViewModelEndpointParameterAttribute> parameterAttributes,
        IDataWrapper sender)
    {
        try
        {
            return GetServerUrl(endpointAttribute, parameterAttributes, sender);
        }
        catch(Exception ex)
        {
            request.MarkErrored(LogEvents.ViewModelRequest_HttpRefresh_RouteConstruction_Error);
            Log.ViewModelRequestHttpRefreshRouteConstructionError(_logger, ex, request.DisplayName, request);
            throw;
        }
    }

    private static string GetServerUrl(
        ViewModelEndpointAttribute endpointAttribute,
        IEnumerable<ViewModelEndpointParameterAttribute> parameterAttributes,
        IDataWrapper sender)
    {

        if(endpointAttribute == null)
            throw new InvalidOperationException($"The {nameof(ViewModelEndpointAttribute)} attribute is missing from the component");

        var result = endpointAttribute.Route;

        foreach(var attribute in parameterAttributes)
        {
            var value = string.Empty;
            value = attribute.ParameterType switch
            {
                DataEndpointParameterType.StateStoreParameter => sender.GetType().GetProperty(attribute.DestinationPropertyName).GetValue(sender.State).ToString(),
                DataEndpointParameterType.ComponentParameter => sender.GetType().GetProperty(attribute.DestinationPropertyName).GetValue(sender).ToString(),
                _ => throw new Exception("Unsupported DataEndpoint Parameter Type"),
            };
            result = result.Replace("{" + attribute.Name + "}", value);
        }

        return result;
    }
}
