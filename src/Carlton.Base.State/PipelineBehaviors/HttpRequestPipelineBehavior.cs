using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Net.Http.Json;

namespace Carlton.Base.State;

public class HttpRequestPipelineBehaviorBase<TRequest, TViewModel, TStateStore> : IPipelineBehavior<TRequest, TViewModel>
    where TRequest : ViewModelRequest<TViewModel>
    where TStateStore : Enum
{
    protected HttpClient Client { get; init; }
    protected IStateAdapter<TViewModel> Adapter { get; init; }
    protected IStateStore<TStateStore> State { get; init; }
    protected ILogger<TViewModel> Logger { get; init; }


    protected HttpRequestPipelineBehaviorBase(HttpClient client,
        IStateAdapter<TViewModel> adapter,
        IStateStore<TStateStore> state,
        ILogger<TViewModel> logger)
    {
        Client = client;
        Adapter = adapter;
        State = state;
        Logger = logger;
    }

    public virtual async Task<TViewModel> Handle(TRequest request, RequestHandlerDelegate<TViewModel> next, CancellationToken cancellationToken)
    {
        var attributes = request.Sender.GetType().CustomAttributes;
        var refreshPolicyAttribute = attributes.OfType<DataEndpointRefreshPolicyAttribute>().FirstOrDefault();
        var urlAttribute = attributes.OfType<DataEndpointAttribute>().FirstOrDefault();
        var urlParameterAttributes = attributes.OfType<DataEndpointParameterAttribute>() ?? new List<DataEndpointParameterAttribute>();
        var requiresRefresh = GetRefreshPolicy(refreshPolicyAttribute);

        if(requiresRefresh)
        {
            var serverUrl = GetServerUrl(urlAttribute, urlParameterAttributes, request.Sender);
            var viewModel = await Client.GetFromJsonAsync<TViewModel>(serverUrl, cancellationToken: cancellationToken);
            Adapter.SaveViewModel(viewModel);
        }

        return await next();
    }

    private bool GetRefreshPolicy(DataEndpointRefreshPolicyAttribute attribute)
    {
        if(attribute == null)
            throw new InvalidOperationException();


        return attribute.DataEndpointRefreshPolicy switch
        {
            DataEndpointRefreshPolicy.Never => false,
            DataEndpointRefreshPolicy.Always => true,
            DataEndpointRefreshPolicy.InitOnly => Adapter.IsEmpty,
            DataEndpointRefreshPolicy.Expired => throw new NotImplementedException(),
            _ => throw new ArgumentException(),
        };
    }

    private string GetServerUrl(DataEndpointAttribute endpointAttribute,
        IEnumerable<DataEndpointParameterAttribute> parameterAttributes,
        object sender)
    {
        if(endpointAttribute == null)
            throw new InvalidOperationException();

        var result = endpointAttribute.Route;

        foreach(var attribute in parameterAttributes)
        {
            var value = string.Empty;
            value = attribute.ParameterType switch
            {
                DataEndpointParameterType.StateStoreParameter => sender.GetType().GetProperty(attribute.DestinationPropertyName).GetValue(State).ToString(),
                DataEndpointParameterType.ComponentParameter => sender.GetType().GetProperty(attribute.DestinationPropertyName).GetValue(sender).ToString(),
                _ => throw new Exception(),
            };
            result = result.Replace("{" + attribute.Name + "}", value);
        }


        return result;
    }
}
