namespace Carlton.Core.Flux.Models;

public class ViewModelQueryContext<TViewModel> : BaseRequestContext
{    
    public TViewModel ResultViewModel { get; private set; }
    public string ViewModelType { get => typeof(TViewModel).GetDisplayName(); }
    public bool IsInitializationRequest { get; init; }
    public bool StateModifiedByHttpRefresh { get; private set; }

    internal void MarkAsStateModifiedByHttpRefresh()
        => StateModifiedByHttpRefresh = true;

    internal void MarkAsSucceeded(TViewModel resultViewModel)
    {
        ResultViewModel = resultViewModel;
        base.MarkAsSucceeded();
    }

    private ViewModelQueryContext()
    {
    }

    public static ViewModelQueryContext<TViewModel> CreateChildViewModelQueryContext(Guid parentId)
    {
        return new ViewModelQueryContext<TViewModel> 
        {
            IsInitializationRequest = false,
            ParentRequestId = parentId
        };
    }

    public static ViewModelQueryContext<TViewModel> CreateViewModelQueryContext()
    {
        return new ViewModelQueryContext<TViewModel> { IsInitializationRequest = true };
    }

    public static ViewModelQueryContext<TViewModel> CreateViewModelQueryContext(bool isInitialRequest)
    {
        return new ViewModelQueryContext<TViewModel> { IsInitializationRequest = isInitialRequest };
    }
}
