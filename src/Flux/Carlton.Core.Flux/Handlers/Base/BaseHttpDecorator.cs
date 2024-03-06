using System.Text;
using System.Text.RegularExpressions;
using Carlton.Core.Flux.Attributes;
using Carlton.Core.Flux.Dispatchers;
namespace Carlton.Core.Flux.Handlers.Base;

public abstract partial class BaseHttpDecorator<TState>(HttpClient _client, IFluxState<TState> _state)
{
    protected HttpClient Client { get; init; } = _client;
    protected IFluxState<TState> State { get; init; } = _state;

    protected Result<string, HttpUrlConstructionError> GetServerUrl(
        HttpRefreshAttribute endpointAttribute,
        IEnumerable<HttpRefreshParameterAttribute> parameterAttributes,
        object sender,
        BaseRequestContext context)
    {
        var result = endpointAttribute.Route;

        foreach (var attribute in parameterAttributes)
        {
            var value = GetDataEndPointParameterType(sender, attribute);
            result = result.Replace($"{{{attribute.Name}}}", value);
        }


        return VerifyUrlParameters(result, context);
    }
   
    private static Result<string, HttpUrlConstructionError> VerifyUrlParameters(string url, BaseRequestContext context)
    {
        var isUrlWellFormed = Uri.IsWellFormedUriString(url, UriKind.Absolute);
        
        //The URL is valid
        if (isUrlWellFormed)
            return url;

        var match = UrlParameterTokenRegex().Match(url);
        var unreplacedTokens = match.Success;

        //The URL is malformed
        if (!unreplacedTokens)
            return new HttpUrlConstructionError(url, context);

        //If the URL is malformed specifically because tokens were not replaced
        var msgBuilder = new StringBuilder(FluxLogs.InvalidRefreshUrlParametersMsg);
 
        while (match.Success)
        {
            msgBuilder.Append($"{match.Value}, ");
            match = match.NextMatch();
        }

        var msg = msgBuilder.ToString().TrimTrailingComma();
        return new HttpUrlConstructionError(url, context);
    }

    protected static bool GetRefreshPolicy(HttpRefreshAttribute attribute)
    {
        return attribute?.DataRefreshPolicy switch
        {
            DataEndpointRefreshPolicy.Never => false,
            DataEndpointRefreshPolicy.Always => true,
            _ => false
        };
    }

    private string GetDataEndPointParameterType(object sender, HttpRefreshParameterAttribute attribute)
    {
        return attribute.ParameterType switch
        {
            DataEndpointParameterType.StateStoreParameter => State.GetType().GetProperty(attribute.DestinationPropertyName).GetValue(State).ToString(),
            DataEndpointParameterType.ComponentParameter => sender.GetType().GetProperty(attribute.DestinationPropertyName).GetValue(sender).ToString(),
            _ => string.Empty
        };
    }

    [GeneratedRegex("\\{[^}]+\\}")]
    private static partial Regex UrlParameterTokenRegex();
}
