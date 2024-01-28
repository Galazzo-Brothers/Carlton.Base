using Carlton.Core.Utilities.JsonConverters;
using System.Text.Json.Serialization;

namespace Carlton.Core.Flux.Models;

public class ViewModelQueryContext<TViewModel> : BaseRequestContext
{    
    public TViewModel ResultViewModel { get; private set; }
    [JsonConverter(typeof(JsonTypeConverter))]
    public string ViewModelType { get => typeof(TViewModel).GetDisplayName(); }
    public bool StateModifiedByHttpRefresh { get; private set; }

    internal void MarkAsStateModifiedByHttpRefresh()
        => StateModifiedByHttpRefresh = true;

    internal void MarkAsSucceeded(TViewModel resultViewModel)
    {
        ResultViewModel = resultViewModel;
        base.MarkAsSucceeded();
    }
}
