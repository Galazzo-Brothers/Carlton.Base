namespace Carlton.Base.TestBed;

public sealed class SourceViewerViewModelRequestHandler : TestBedRequestHandlerBase, IRequestHandler<ViewModelRequest<SourceViewerViewModel>, SourceViewerViewModel>
{
    private readonly IJSRuntime _jsRuntime;

    public SourceViewerViewModelRequestHandler(TestBedState state, IJSRuntime jsRuntime) : base(state)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task<SourceViewerViewModel> Handle(ViewModelRequest<SourceViewerViewModel> request, CancellationToken cancellationToken)
    {
        await using var module = await _jsRuntime.InvokeAsync<IJSObjectReference>(JavaScriptHelper.Import, JavaScriptHelper.GetImportPath(nameof(SourceViewer)));
        var markup = await module.InvokeAsync<string>(SourceViewer.GetOutputSource);
        return new SourceViewerViewModel(markup);
    }
}

