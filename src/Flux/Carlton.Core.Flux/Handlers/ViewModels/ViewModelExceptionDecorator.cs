using Carlton.Core.Flux.Exceptions;
namespace Carlton.Core.Flux.Handlers.ViewModels;

public class ViewModelExceptionDecorator<TState>(IViewModelQueryDispatcher<TState> decorated) : IViewModelQueryDispatcher<TState>
{
    private readonly IViewModelQueryDispatcher<TState> _decorated = decorated;

    public async Task<TViewModel> Dispatch<TViewModel>(object sender, ViewModelQueryContext<TViewModel> context, CancellationToken cancellationToken)
    {
        try
        {
            var viewmodel = await _decorated.Dispatch(sender, context, cancellationToken);
            return viewmodel;
        }
        catch (Exception ex)
        {
            context.MarkAsErrored(ex);
            throw ViewModelFluxException<TState, TViewModel>.UnhandledError(context, ex);
        }
    }
}
