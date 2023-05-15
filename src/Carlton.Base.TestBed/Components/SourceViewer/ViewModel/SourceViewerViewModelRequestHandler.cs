namespace Carlton.Base.TestBed;

public sealed class SourceViewerViewModelRequestHandler : IRequestHandler<ViewModelRequest<SourceViewerViewModel>, SourceViewerViewModel>
{
    private readonly IJSRuntime _jsRuntime;

    public SourceViewerViewModelRequestHandler(IJSRuntime jsRuntime) 
    {
        _jsRuntime = jsRuntime;
    }

    public async Task<SourceViewerViewModel> Handle(ViewModelRequest<SourceViewerViewModel> request, CancellationToken cancellationToken)
    {
        const string QuerySelector = ".component-viewer";
        await using var module = await _jsRuntime.InvokeAsync<IJSObjectReference>(JavaScriptHelper.Import, JavaScriptHelper.GetImportPath(typeof(Components.SourceViewer)));
        var markup = await module.InvokeAsync<string>(Carlton.Base.Components.SourceViewer.GetOutputSource, QuerySelector);

        return new SourceViewerViewModel(markup);
    }
}

