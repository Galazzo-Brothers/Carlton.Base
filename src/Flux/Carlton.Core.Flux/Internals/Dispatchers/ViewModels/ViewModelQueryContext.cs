using Carlton.Core.Utilities.JsonConverters;
using System.Text.Json.Serialization;

namespace Carlton.Core.Flux.Internals.Dispatchers.ViewModels;

internal class ViewModelQueryContext<TViewModel> : BaseRequestContext
{
	public TViewModel ResultViewModel { get; private set; }
	public override FluxOperationKind FluxOperationKind => FluxOperationKind.ViewModelQuery;
	[JsonConverter(typeof(JsonTypeConverter))]
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

