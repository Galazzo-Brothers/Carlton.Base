using System.Net;
using System.Net.Http.Json;
using Carlton.Core.Flux.Attributes;
using Carlton.Core.Flux.Handlers.Base;
using Carlton.Core.Flux.State;

namespace Carlton.Core.Flux.Handlers.ViewModels;

public class ViewModelHttpDecorator<TState>(
    IViewModelQueryDispatcher<TState> _decorated, HttpClient client, TState state)
    : BaseHttpDecorator<TState>(client, state), IViewModelQueryDispatcher<TState>
{
    public async Task<TViewModel> Dispatch<TViewModel>(object sender, ViewModelQueryContext<TViewModel> context, CancellationToken cancellationToken)
    {
        //Get RefreshPolicy Attribute
        var attributes = sender.GetType().GetCustomAttributes();
        var httpRefreshAttribute = attributes.OfType<ViewModelHttpRefreshAttribute>().FirstOrDefault();
        var requiresRefresh = GetRefreshPolicy(httpRefreshAttribute);

        if(requiresRefresh)
        {
            //Update the context for logging and auditing
            context.MarkAsRequiresHttpRefresh();

            //Construct Http Refresh URL
            var urlParameterAttributes = attributes.OfType<HttpRefreshParameterAttribute>() ?? new List<HttpRefreshParameterAttribute>();
            var serverUrl = GetServerUrl(httpRefreshAttribute, urlParameterAttributes, sender);

            //Http Refresh ViewModel
            var viewModel = await _client.GetFromJsonAsync<TViewModel>(serverUrl, cancellationToken);
            context.MarkAsHttpCallMade(serverUrl, HttpStatusCode.OK, viewModel);

            //Update the StateStore
            var command = new ViewModelRemoteRefreshCommand<TViewModel>(viewModel);
            var commandContext = new MutationCommandContext<ViewModelRemoteRefreshCommand<TViewModel>>(command);
            // await _state.ApplyMutationCommand(commandContext);
            context.MarkAsStateModifiedByHttpRefresh();
        }

        return await _decorated.Dispatch(sender, context, cancellationToken);
    }
}
