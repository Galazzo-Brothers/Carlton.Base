using Carlton.Core.Components.Flux.Attributes;
using System.Text;
using System.Text.RegularExpressions;

namespace Carlton.Core.Components.Flux.Decorators.Base;

public abstract class BaseHttpDecorator<TState>
{
    protected readonly HttpClient _client;
    protected readonly IMutableFluxState<TState> _fluxState;

    protected BaseHttpDecorator(HttpClient client, IMutableFluxState<TState> fluxState)
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
        var msgBuilder = new StringBuilder("The HTTP ViewModel refresh endpoint is invalid, following URL parameters were not replaced: ");

        //Check for any unreplaced parameters
        var match = new Regex("\\{[^}]+\\}").Match(url);

        //If there are none continue
        if (!match.Success)
            return;

        while (match.Success)
        {
            msgBuilder.Append($"{match.Value}, ");
            match = match.NextMatch();
        }

        var exMessage = msgBuilder.ToString().TrimTrailingComma();
        throw new InvalidOperationException(exMessage);
    }
}
