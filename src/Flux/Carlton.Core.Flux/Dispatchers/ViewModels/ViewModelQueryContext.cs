namespace Carlton.Core.Flux.Dispatchers.ViewModels;

public class ViewModelQueryContext<TViewModel> : BaseRequestContext
{
	public TViewModel ResultViewModel { get; private set; }
	public override FluxOperation FluxOperation => FluxOperation.ViewModelQuery;
	public override Type FluxOperationType => typeof(TViewModel);
	public bool StateModifiedByHttpRefresh { get; private set; }

	internal void MarkAsStateModifiedByHttpRefresh()
		=> StateModifiedByHttpRefresh = true;

	internal void MarkAsSucceeded(TViewModel resultViewModel)
	{
		ResultViewModel = resultViewModel;
		MarkAsSucceeded();
	}
}
