using System.Net.Http.Json;
using Carlton.Core.Flux.Attributes;
using Carlton.Core.Flux.Handlers.Base;
namespace Carlton.Core.Flux.Dispatchers.ViewModels.Decorators;

public class ViewModelHttpDecorator<TState>(
    IViewModelQueryDispatcher<TState> _decorated,
    HttpClient _client,
    IMutableFluxState<TState> _state)
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
            var vmResult = await serverUrlResult.Match
                (
                    async serverUrl =>
                    {
                        //Get ViewModel from server
                        var vmResult = await GetHttpViewModel(serverUrl, context, cancellationToken);

                        //Update the StateStore and pickup any mutation errors
                        vmResult = await ApplyViewModelStateMutation(vmResult, context);

                        //Return result
                        return vmResult;
                    },
                    err => err.ToResultTask<TViewModel, FluxError>()
                );

            //If http refresh failed return error
            if (!vmResult.IsSuccess)
                return vmResult;
        }

        //Continue with Dispatch
        return await _decorated.Dispatch(sender, context, cancellationToken);
    }

    private async Task<Result<TViewModel, FluxError>> GetHttpViewModel<TViewModel>(string serverUrl, ViewModelQueryContext<TViewModel> context, CancellationToken cancellationToken)
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
    }

    private async Task<Result<TViewModel, FluxError>> ApplyViewModelStateMutation<TViewModel>(Result<TViewModel, FluxError> vmResult, ViewModelQueryContext<TViewModel> context)
    {
        return await vmResult.Match<Task<Result<TViewModel, FluxError>>>
        (
            async vm =>
            {
                await _state.ApplyMutationCommand(vm);
                context.MarkAsStateModifiedByHttpRefresh();
                return vm;
            },
            err => err.ToResultTask<TViewModel, FluxError>()
        );
    }
}
