using Carlton.Core.Components.Flux.Decorators.Base;
using System.Net.Http.Json;

namespace Carlton.Core.Components.Flux.Decorators.ViewModels;

public class ViewModelHttpDecorator<TState> : BaseHttpDecorator<TState>, IViewModelQueryDispatcher<TState>
{
    private readonly IViewModelQueryDispatcher<TState> _decorated;
    private readonly ILogger<ViewModelHttpDecorator<TState>> _logger;

    public ViewModelHttpDecorator(
        IViewModelQueryDispatcher<TState> decorated,
        HttpClient client,
        IMutableFluxState<TState> fluxState,
        ILogger<ViewModelHttpDecorator<TState>> logger) :base(client, fluxState)
        => (_decorated, _logger) = (decorated, logger);

    public async Task<TViewModel> Dispatch<TViewModel>(ViewModelQuery query, CancellationToken cancellationToken)
    {
        //Get RefreshPolicy Attribute
        var attributes = query.Sender.GetType().GetCustomAttributes();
        var httpRefreshAttribute = attributes.OfType<ViewModelHttpRefreshAttribute>().FirstOrDefault();
        var requiresRefresh =  GetRefreshPolicy(httpRefreshAttribute);
        var vmType = typeof(TViewModel).GetDisplayName();

        if(requiresRefresh)
        {
            //Log HttpRefresh Process
            Log.ViewModelHttpRefreshStarted(_logger, vmType);

            //Construct Http Refresh URL
            var urlParameterAttributes = attributes.OfType<HttpRefreshParameterAttribute>() ?? new List<HttpRefreshParameterAttribute>();
            var serverUrl = GetServerUrl(httpRefreshAttribute, urlParameterAttributes, query.Sender);

            //Http Refresh ViewModel
            var viewModel = await _client.GetFromJsonAsync<TViewModel>(serverUrl, cancellationToken);

            //Update the StateStore
            await _fluxState.MutateState(viewModel);

            //Logging and Auditing 
            Log.ViewModelHttpRefreshCompleted(_logger, vmType);
        }
        else
        {
            Log.ViewModelHttpRefreshSkipped(_logger, vmType);
        }

        return await _decorated.Dispatch<TViewModel>(query, cancellationToken);
    }
}
