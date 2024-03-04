using System.Text;
using System.Text.RegularExpressions;
using Carlton.Core.Flux.Attributes;

namespace Carlton.Core.Flux.Handlers.Base;

public abstract partial class BaseHttpDecorator<TState>(HttpClient _client, IFluxState<TState> _state)
{
    protected HttpClient Client { get; init; } = _client;
    protected IFluxState<TState> State { get; init; } = _state;

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
                DataEndpointParameterType.StateStoreParameter => State.GetType().GetProperty(attribute.DestinationPropertyName).GetValue(State).ToString(),
                DataEndpointParameterType.ComponentParameter => sender.GetType().GetProperty(attribute.DestinationPropertyName).GetValue(sender).ToString(),
                _ => throw new InvalidOperationException(FluxLogs.InvalidRefreshUrlCreationEnumValueMsg)
            };
            result = result.Replace($"{{{attribute.Name}}}", value);
        }

        VerifyUrlParameters(result);
        return result;
    }

    private static void VerifyUrlParameters(string url)
    {
        var isUrlWellFormed = Uri.IsWellFormedUriString(url, UriKind.Absolute);
        var msgBuilder = new StringBuilder(FluxLogs.InvalidRefreshUrlParametersMsg);

        //Check for any unreplaced parameters
        var match = UrlParameterTokenRegex().Match(url);
        var unreplacedTokens = match.Success;

        //If the URL is well formed return
        if (isUrlWellFormed)
            return;

        //If there are no unreplaced tokens throw an exception for HttpRefreshAttribute
        if (!unreplacedTokens)
            throw new InvalidOperationException(FluxLogs.InvalidRefreshUrlMsg);

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
