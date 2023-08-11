using Carlton.Core.InProcessMessaging.Queries;

namespace Carlton.Core.Components.Flux.Contracts;

public interface IViewModelQueryHandler<TState, TViewModel> : IQueryHandler<ViewModelQuery, TViewModel>
{
}
