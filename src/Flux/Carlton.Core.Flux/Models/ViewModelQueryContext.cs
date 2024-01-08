namespace Carlton.Core.Flux.Models;

public class ViewModelQueryContext<TViewModel> : BaseRequestContext
{    
    public string ViewModelType { get => typeof(TViewModel).GetDisplayName(); }
}
