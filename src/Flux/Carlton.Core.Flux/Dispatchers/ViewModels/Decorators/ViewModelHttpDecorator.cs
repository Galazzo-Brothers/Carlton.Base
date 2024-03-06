using System.Net.Http.Json;
using Carlton.Core.Flux.Attributes;
using Carlton.Core.Flux.Handlers.Base;
namespace Carlton.Core.Flux.Dispatchers.ViewModels.Decorators;

public class ViewModelHttpDecorator<TState>(
    IViewModelQueryDispatcher<TState> _decorated, HttpClient _client, IMutableFluxState<TState> _state)
    : BaseHttpDecorator<TState>(_client, _state), IViewModelQueryDispatcher<TState>
{
    public async Task<Result<TViewModel, FluxError>> Dispatch<TViewModel>(object sender, ViewModelQueryContext<TViewModel> context, CancellationToken cancellationToken)
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
            var serverUrlResult = GetServerUrl(httpRefreshAttribute, urlParameterAttributes, sender, context);

            //Get ViewModel from server
            var vmResult = await GetHttpViewModel(serverUrlResult, context, cancellationToken);

            //Update the StateStore
            await ApplyViewModelStateMutation(vmResult, context);
        }

        //Continue with Dispatch
        return await _decorated.Dispatch(sender, context, cancellationToken);
    }

    private async Task<Result<TViewModel, FluxError>> GetHttpViewModel<TViewModel>(Result<string, FluxError> serverUrlResult, ViewModelQueryContext<TViewModel> context, CancellationToken cancellationToken)
    {
        return await serverUrlResult.Match
        (
            async serverUrl =>
            {
                try
                {
                    var response = await Client.GetAsync(serverUrl, cancellationToken);

                    if (!response.IsSuccessStatusCode)
                        return HttpRequestFailedError(response);

                    // Deserialize the content to the specified type
                    var viewModel = await response.Content.ReadFromJsonAsync<TViewModel>(cancellationToken: cancellationToken);

                    context.MarkAsHttpCallMade(serverUrl, System.Net.HttpStatusCode.OK, viewModel);
                    return viewModel;
                }
                catch (HttpRequestException ex)
                {
                    return HttpError(ex);
                }
                catch (JsonException ex)
                {
                    return JsonError(ex);
                }
                catch (NotSupportedException ex) when (ex.Message.Contains("Serialization and deserialization"))
                {
                    return JsonError(ex);
                }
            },
            err => err.ToResultTask<TViewModel, FluxError>()
        );
    }

    private async Task ApplyViewModelStateMutation<TViewModel>(Result<TViewModel, FluxError> vmResult, ViewModelQueryContext<TViewModel> context)
    {
        await vmResult.Match
        (
            async vm =>
            {
                await _state.ApplyMutationCommand(vm);
                context.MarkAsStateModifiedByHttpRefresh();
            },
            err => err.ToResultTask<TViewModel, FluxError>()
        );
    }
}
