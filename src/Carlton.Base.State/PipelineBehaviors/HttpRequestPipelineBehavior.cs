using System.Net.Http;
using System.Net.Http.Json;

namespace Carlton.Base.State;

public abstract class HttpRequestPipelineBehaviorBase<TRequest, TViewModel> : IPipelineBehavior<TRequest, TViewModel>
    where TRequest : ViewModelRequest<TViewModel>
{
    protected HttpClient Client { get; init; }
    protected ICommandProcessor CommandProcessor { get; init; }

    protected HttpRequestPipelineBehaviorBase(HttpClient client, ICommandProcessor commandProcessor)
    {
        Client = client;
        CommandProcessor = commandProcessor;
    }

    public virtual async Task<TViewModel> Handle(TRequest request, RequestHandlerDelegate<TViewModel> next, CancellationToken cancellationToken)
    {
        var attributes = request.Sender.GetType().GetCustomAttributes();
        var refreshPolicyAttribute = attributes.OfType<DataEndpointRefreshPolicyAttribute>().FirstOrDefault();
        var urlAttribute = attributes.OfType<DataRefreshEndpointAttribute>().FirstOrDefault();
        var urlParameterAttributes = attributes.OfType<DataEndpointParameterAttribute>() ?? new List<DataEndpointParameterAttribute>();
        var requiresRefresh = GetRefreshPolicy(refreshPolicyAttribute);

        if(requiresRefresh)
        {
            var serverUrl = GetServerUrl(urlAttribute, urlParameterAttributes, request.Sender);
            var viewModel = await Client.GetFromJsonAsync<TViewModel>(serverUrl, cancellationToken: cancellationToken);
            viewModel.Adapt(request.Sender.State);
            refreshPolicyAttribute.InitialRequestOccured = true;
            request.MarkAsServerCalled();
        }


        return await next();
    }

    private static bool GetRefreshPolicy(DataEndpointRefreshPolicyAttribute attribute)
    {
        if(attribute == null)
            throw new InvalidOperationException();


        return attribute.DataEndpointRefreshPolicy switch
        {
            DataEndpointRefreshPolicy.Never => false,
            DataEndpointRefreshPolicy.Always => true,
            DataEndpointRefreshPolicy.InitOnly => attribute.InitialRequestOccured,
            DataEndpointRefreshPolicy.Expired => throw new NotImplementedException(),
            _ => throw new ArgumentException("Unsupported DataEndpoint Refresh Policy"),
        };
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
