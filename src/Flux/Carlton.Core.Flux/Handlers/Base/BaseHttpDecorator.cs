using Carlton.Core.Flux.Attributes;
using System.Text;
using System.Text.RegularExpressions;

namespace Carlton.Core.Flux.Handlers.Base;

public abstract partial class BaseHttpDecorator<TState>
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
                _ => throw new InvalidOperationException(LogEvents.InvalidRefreshUrlCreationEnumValueMsg)
            };
            result = result.Replace("{" + attribute.Name + "}", value);
        }

        VerifyUrlParameters(result);
        return result;
    }

    private static void VerifyUrlParameters(string url)
    {
        var isUrlWellFormed = Uri.IsWellFormedUriString(url, UriKind.Absolute);
        var msgBuilder = new StringBuilder(LogEvents.InvalidRefreshUrlParametersMsg);

        //Check for any unreplaced parameters
        var match = UrlParameterTokenRegex().Match(url);
        var unreplacedTokens = match.Success;


        //If the URL is well formed return
        if (isUrlWellFormed)
            return;

        //If there are no unreplaced tokens throw an exception for HttpRefreshAttribute
        if (!unreplacedTokens)
            throw new InvalidOperationException(LogEvents.InvalidRefreshUrlMsg);

        //If there are unreplaced tokens throw an exception for HttpRefreshParameterAttribute
        while (match.Success)
        {
            msgBuilder.Append($"{match.Value}, ");
            match = match.NextMatch();
        }

        var exMessage = msgBuilder.ToString().TrimTrailingComma();
        throw new InvalidOperationException(exMessage);
    }

    [GeneratedRegex("\\{[^}]+\\}")]
    private static partial Regex UrlParameterTokenRegex();
}
