using Carlton.Core.Components.Flux.Attributes;
using System.Net.Http.Json;
using System.Text.RegularExpressions;

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
        var httpRefreshAttribute = attributes.OfType<ViewModelHttpRefreshAttribute>().FirstOrDefault();
        var requiresRefresh =  GetRefreshPolicy<TViewModel>(httpRefreshAttribute);
        var vmType = typeof(TViewModel).GetDisplayName();

        if(requiresRefresh)
        {
            //Log HttpRefresh Process
            Log.ViewModelHttpRefreshStarted(_logger, vmType);

            //Construct Http Refresh URL
            var urlParameterAttributes = attributes.OfType<ViewModelHttpRefreshParameterAttribute>() ?? new List<ViewModelHttpRefreshParameterAttribute>();
            var serverUrl = GetServerUrl(httpRefreshAttribute, urlParameterAttributes, query.Sender);

            //Http Refresh ViewModel
            var viewModel = await _client.GetFromJsonAsync<TViewModel>(serverUrl, cancellationToken);

            //Update the StateStore
            viewModel.Adapt(_fluxState.State);

            //Logging and Auditing 
            Log.ViewModelHttpRefreshCompleted(_logger, vmType);
        }
        else
        {
            Log.ViewModelHttpRefreshSkipped(_logger, vmType);
        }

        return await _decorated.Dispatch<TViewModel>(query, cancellationToken);
    }

    private static bool GetRefreshPolicy<TViewModel>(ViewModelHttpRefreshAttribute attribute)
    {
        return attribute?.DataRefreshPolicy switch
        {
            DataEndpointRefreshPolicy.Never => false,
            DataEndpointRefreshPolicy.Always => true,
            _ => false
        };
    }

    private string GetServerUrl(
        ViewModelHttpRefreshAttribute endpointAttribute,
        IEnumerable<ViewModelHttpRefreshParameterAttribute> parameterAttributes,
        object sender)
    {
        var result = endpointAttribute.Route;

        foreach(var attribute in parameterAttributes)
        {
            var value = string.Empty;
            value = attribute.ParameterType switch
            {
                DataEndpointParameterType.StateStoreParameter => sender.GetType().GetProperty(attribute.DestinationPropertyName).GetValue(_fluxState.State).ToString(),
                DataEndpointParameterType.ComponentParameter => sender.GetType().GetProperty(attribute.DestinationPropertyName).GetValue(sender).ToString(),
                _ => throw new Exception("Unsupported DataEndpoint Parameter Type"),
            };
            result = result.Replace("{" + attribute.Name + "}", value);
        }

        VerifyUrlParameters(result);

       

        return result;
    }

    private static void VerifyUrlParameters(string url)
    {
        var message = "The HTTP ViewModel refresh endpoint is invalid, following URL parameters were not replaced: ";
     
        //Check for any unreplaced parameters
        var match = new Regex("\\{[^}]+\\}").Match(url);

        //If there are none continue
        if (!match.Success)
            return;

        while(match.Success)
        {
            message += match.Value + ", ";

            match = match.NextMatch();
        }

        message = message.TrimTrailingComma();

        throw new InvalidOperationException(message);
    }
}
