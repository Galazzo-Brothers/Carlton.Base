using Carlton.Core.Components.Flux.Attributes;
using Carlton.Core.Components.Flux.State;
using System.Net.Http.Json;

namespace Carlton.Core.Components.Flux.Decorators.ViewModels;

public class ViewModelHttpDecorator<TState> : IViewModelQueryDispatcher<TState>
{
    private readonly IViewModelQueryDispatcher<TState> _decorated;
    private readonly HttpClient _client;
    private readonly IFluxState<TState> _fluxState;
    private readonly ILogger<ViewModelHttpDecorator<TState>> _logger;

    public ViewModelHttpDecorator(IViewModelQueryDispatcher<TState> decorated, HttpClient client, IFluxState<TState> fluxState, ILogger<ViewModelHttpDecorator<TState>> logger)
        => (_decorated, _client, _fluxState, _logger) = (decorated, client, fluxState, logger);

    public async Task<TViewModel> Dispatch<TViewModel>(ViewModelQuery query, CancellationToken cancellationToken)
    {
        //Get RefreshPolicy Attribute
        var attributes = query.Sender.GetType().GetCustomAttributes();
        var refreshPolicyAttribute = attributes.OfType<ViewModelEndpointRefreshPolicyAttribute>().FirstOrDefault();
        var requiresRefresh = GetRefreshPolicy(refreshPolicyAttribute);
        var vmType = typeof(TViewModel).GetDisplayName();

        if(requiresRefresh)
        {
            //Log HttpRefresh Process
            Log.ViewModelHttpRefreshStarted(_logger, vmType);

            //Construct Http Refresh URL
            var urlAttribute = attributes.OfType<ViewModelEndpointAttribute>().FirstOrDefault();
            var urlParameterAttributes = attributes.OfType<ViewModelEndpointParameterAttribute>() ?? new List<ViewModelEndpointParameterAttribute>();
            var serverUrl = GetServerUrl(urlAttribute, urlParameterAttributes, query.Sender);

            //Http Refresh ViewModel
            var viewModel = await _client.GetFromJsonAsync<TViewModel>(serverUrl, cancellationToken);
            
            //Update the StateStore
            viewModel.Adapt(_fluxState.State);

            //Logging and Auditing 
            refreshPolicyAttribute.InitialRequestOccurred = true;
            Log.ViewModelHttpRefreshCompleted(_logger, vmType);
        }
        else
        {
            Log.ViewModelHttpRefreshSkipped(_logger, vmType);
        }

        return await _decorated.Dispatch<TViewModel>(query, cancellationToken);
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

    private static string GetServerUrl(
        ViewModelEndpointAttribute endpointAttribute,
        IEnumerable<ViewModelEndpointParameterAttribute> parameterAttributes,
        object sender)
    {

        if(endpointAttribute == null)
            throw new InvalidOperationException($"The {nameof(ViewModelEndpointAttribute)} attribute is missing from the component");

        var result = endpointAttribute.Route;

        foreach(var attribute in parameterAttributes)
        {
            var value = string.Empty;
            value = attribute.ParameterType switch
            {
               // DataEndpointParameterType.StateStoreParameter => sender.GetType().GetProperty(attribute.DestinationPropertyName).GetValue(sender.State).ToString(),
                DataEndpointParameterType.ComponentParameter => sender.GetType().GetProperty(attribute.DestinationPropertyName).GetValue(sender).ToString(),
                _ => throw new Exception("Unsupported DataEndpoint Parameter Type"),
            };
            result = result.Replace("{" + attribute.Name + "}", value);
        }

        return result;
    }
}
