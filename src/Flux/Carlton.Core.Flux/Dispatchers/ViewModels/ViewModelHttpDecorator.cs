using System.Net;
using System.Net.Http.Json;
using Carlton.Core.Flux.Attributes;
using Carlton.Core.Flux.Handlers.Base;
namespace Carlton.Core.Flux.Dispatchers.ViewModels;

public class ViewModelHttpDecorator<TState>(
    IViewModelQueryDispatcher<TState> _decorated, HttpClient _client, IMutableFluxState<TState> _state)
    : BaseHttpDecorator<TState>(_client, _state), IViewModelQueryDispatcher<TState>
{
    public Task<Result<TViewModel, ViewModelFluxError>> Dispatch<TViewModel>(object sender, ViewModelQueryContext<TViewModel> context, CancellationToken cancellationToken)
    {
        //Get RefreshPolicy Attribute
        var attributes = sender.GetType().GetCustomAttributes();
        var httpRefreshAttribute = attributes.OfType<ViewModelHttpRefreshAttribute>().FirstOrDefault();
        var requiresRefresh = GetRefreshPolicy(httpRefreshAttribute);

        if (requiresRefresh)
        {
            //Update the context for logging and auditing
            context.MarkAsRequiresHttpRefresh();

            //Construct Http Refresh URL
            var urlParameterAttributes = attributes.OfType<HttpRefreshParameterAttribute>() ?? new List<HttpRefreshParameterAttribute>();
            var serverUrl = GetServerUrl(httpRefreshAttribute, urlParameterAttributes, sender);

            //Http Refresh ViewModel
            var viewModel = Client.GetFromJsonAsync<TViewModel>(serverUrl, cancellationToken);
            context.MarkAsHttpCallMade(serverUrl, HttpStatusCode.OK, viewModel);

            //Update the State
            _state.ApplyMutationCommand(viewModel);

            //Update the StateStore  
            context.MarkAsStateModifiedByHttpRefresh();
        }

        return _decorated.Dispatch(sender, context, cancellationToken);
    }
}
