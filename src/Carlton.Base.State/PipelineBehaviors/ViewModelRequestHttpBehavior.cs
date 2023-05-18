namespace Carlton.Base.State;

public abstract class HttpRequestPipelineBehaviorBase<TRequest, TViewModel> : IPipelineBehavior<TRequest, TViewModel>
    where TRequest : ViewModelRequest<TViewModel>
{
    protected HttpClient Client { get; init; }
    private readonly ILogger<HttpRequestPipelineBehaviorBase<TRequest, TViewModel>> _logger;

    protected HttpRequestPipelineBehaviorBase(HttpClient client, ILogger<HttpRequestPipelineBehaviorBase<TRequest, TViewModel>> logger)
    {
        Client = client;
        _logger = logger;
    }

    public virtual async Task<TViewModel> Handle(TRequest request, RequestHandlerDelegate<TViewModel> next, CancellationToken cancellationToken)
    {
        //Get RefreshPolicy Attribute
        var attributes = request.Sender.GetType().GetCustomAttributes();
        var refreshPolicyAttribute = attributes.OfType<DataEndpointRefreshPolicyAttribute>().FirstOrDefault();
        var requiresRefresh = GetRefreshPolicy(refreshPolicyAttribute);
       
        if(requiresRefresh)
        {
            //Log HttpRefresh Process
            Log.ViewModelRequestHttpRefreshStarting(_logger, request.RequestName, request);
            
            //Construct Http Refresh URL
            var urlAttribute = attributes.OfType<DataRefreshEndpointAttribute>().FirstOrDefault();
            var urlParameterAttributes = attributes.OfType<DataEndpointParameterAttribute>() ?? new List<DataEndpointParameterAttribute>();
            var serverUrl = GetServerUrlWrapperCall(request, urlAttribute, urlParameterAttributes, request.Sender);
            
            //Update State with Http Refresh
            await RefreshViewModel(request, serverUrl, cancellationToken);
            
            //Logging and Auditing 
            refreshPolicyAttribute.InitialRequestOccurred = true;
            request.MarkAsServerCalled();
            Log.ViewModelRequestHttpRefreshCompleted(_logger, request.RequestName, request);
        }
        else
        {
            Log.ViewModelRequestSkippingHttpRefresh(_logger, request.RequestName, request);
        }

        return await next();
    }

    private async Task RefreshViewModel(ViewModelRequest<TViewModel> request, string serverUrl, CancellationToken cancellationToken)
    {
        try
        {
            var viewModel = await Client.GetFromJsonAsync<TViewModel>(serverUrl, cancellationToken);
            viewModel.Adapt(request.State);
        }
        catch(HttpRequestException ex)
        {
            Log.ViewModelRequestHttpRefreshError(_logger, ex, request.RequestName, request);
            throw;
        }
        catch(Exception ex)
        {
            Log.ViewModelRequestHttpRefreshMappingError(_logger, ex, request.RequestName, request);
            throw;
        }
    }

    private static bool GetRefreshPolicy(DataEndpointRefreshPolicyAttribute attribute)
    {
        return attribute?.DataEndpointRefreshPolicy switch
        {
            DataEndpointRefreshPolicy.Never => false,
            DataEndpointRefreshPolicy.Always => true,
            DataEndpointRefreshPolicy.InitOnly => attribute.InitialRequestOccurred,
            _ => false
        };
    }

    private string GetServerUrlWrapperCall(
        ViewModelRequest<TViewModel> request,
        DataRefreshEndpointAttribute endpointAttribute,
        IEnumerable<DataEndpointParameterAttribute> parameterAttributes,
        IDataWrapper sender)
    {
        try
        {
            return GetServerUrl(endpointAttribute, parameterAttributes, sender);
        }
        catch(Exception ex)
        {
            Log.ViewModelRequestHttpRefreshRouteConstructionError(_logger, ex, request.RequestName, request);
            throw;
        }
    }

    private static string GetServerUrl(
        DataRefreshEndpointAttribute endpointAttribute,
        IEnumerable<DataEndpointParameterAttribute> parameterAttributes,
        IDataWrapper sender)
    {

        if(endpointAttribute == null)
            throw new InvalidOperationException($"The {nameof(DataRefreshEndpointAttribute)} attribute is missing from the component");

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
