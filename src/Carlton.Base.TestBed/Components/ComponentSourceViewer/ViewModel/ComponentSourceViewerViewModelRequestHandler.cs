namespace Carlton.Base.TestBed;

public sealed class ComponentSourceViewerViewModelRequestHandler : TestBedRequestHandlerBase, IRequestHandler<ViewModelRequest<ComponentSourceViewerViewModel>, ComponentSourceViewerViewModel>
{
    private readonly IJSRuntime _jsRuntime;

    public ComponentSourceViewerViewModelRequestHandler(TestBedState state, IJSRuntime jsRuntime) : base(state)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task<ComponentSourceViewerViewModel> Handle(ViewModelRequest<ComponentSourceViewerViewModel> request, CancellationToken cancellationToken)
    {
        const string QuerySelector = ".component-viewer";
        await using var module = await _jsRuntime.InvokeAsync<IJSObjectReference>(JavaScriptHelper.Import, JavaScriptHelper.GetImportPath(typeof(SourceViewer)));
        var markup = await module.InvokeAsync<string>(SourceViewer.GetOutputSource, QuerySelector);

        return new ComponentSourceViewerViewModel(markup);
    }
}

