namespace Carlton.Base.TestBed;

public sealed class TestBedSourceViewerViewModelRequestHandler : IRequestHandler<ViewModelRequest<TestBedSourceViewerViewModel>, TestBedSourceViewerViewModel>
{
    private readonly IJSRuntime _jsRuntime;

    public TestBedSourceViewerViewModelRequestHandler(IJSRuntime jsRuntime) 
    {
        _jsRuntime = jsRuntime;
    }

    public async Task<TestBedSourceViewerViewModel> Handle(ViewModelRequest<TestBedSourceViewerViewModel> request, CancellationToken cancellationToken)
    {
        const string QuerySelector = ".component-viewer";
        await using var module = await _jsRuntime.InvokeAsync<IJSObjectReference>(JavaScriptHelper.Import, JavaScriptHelper.GetImportPath(typeof(SourceViewer)));
        var markup = await module.InvokeAsync<string>(SourceViewer.GetOutputSource, QuerySelector);

        return new TestBedSourceViewerViewModel(markup);
    }
}

