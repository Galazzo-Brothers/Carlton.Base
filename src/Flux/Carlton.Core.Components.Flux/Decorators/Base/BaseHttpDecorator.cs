using Carlton.Core.Components.Flux.Attributes;
using System.Text.RegularExpressions;

namespace Carlton.Core.Components.Flux.Decorators.Base;

public abstract class BaseHttpDecorator<TState>
{
    protected readonly HttpClient _client;
    protected readonly IFluxState<TState> _fluxState;

    public BaseHttpDecorator(HttpClient client, IFluxState<TState> fluxState)
        => (_client, _fluxState) = (client, fluxState);

    protected static bool GetRefreshPolicy(HttpRefreshAttribute attribute)
    {
        return attribute?.DataRefreshPolicy switch
        {
            DataEndpointRefreshPolicy.Never => false,
            DataEndpointRefreshPolicy.Always => true,
            _ => false
        };
    }

    protected string GetServerUrl(
        HttpRefreshAttribute endpointAttribute,
        IEnumerable<HttpRefreshParameterAttribute> parameterAttributes,
        object sender)
    {
        var result = endpointAttribute.Route;

        foreach (var attribute in parameterAttributes)
        {
            var value = string.Empty;
            value = attribute.ParameterType switch
            {
                DataEndpointParameterType.StateStoreParameter => _fluxState.State.GetType().GetProperty(attribute.DestinationPropertyName).GetValue(_fluxState.State).ToString(),
                DataEndpointParameterType.ComponentParameter => sender.GetType().GetProperty(attribute.DestinationPropertyName).GetValue(sender).ToString(),
                _ => throw new Exception("Unsupported DataEndpoint Parameter Type"),
            };
            result = result.Replace("{" + attribute.Name + "}", value);
        }

        VerifyUrlParameters(result);



        return result;
    }

    protected static void VerifyUrlParameters(string url)
    {
        var message = "The HTTP ViewModel refresh endpoint is invalid, following URL parameters were not replaced: ";

        //Check for any unreplaced parameters
        var match = new Regex("\\{[^}]+\\}").Match(url);

        //If there are none continue
        if (!match.Success)
            return;

        while (match.Success)
        {
            message += match.Value + ", ";

            match = match.NextMatch();
        }

        message = message.TrimTrailingComma();

        throw new InvalidOperationException(message);
    }
}
