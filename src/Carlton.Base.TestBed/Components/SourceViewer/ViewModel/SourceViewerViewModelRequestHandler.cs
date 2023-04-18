namespace Carlton.Base.TestBedFramework;
public sealed class SourceViewerViewModelRequestHandler : TestBedRequestHandlerViewModelBase<SourceViewerViewModelRequest, SourceViewerViewModel>
{
    private const string PROJECT_NAME = "Carlton.Base.TestBedFramework";
    private readonly IJSRuntime _jsRuntime;

    public SourceViewerViewModelRequestHandler(IJSRuntime jsRuntime, TestBedState state) : base(state)
    {
        _jsRuntime = jsRuntime;
    }

    public async override Task<SourceViewerViewModel> Handle(SourceViewerViewModelRequest request, CancellationToken cancellationToken)
    {
        var module = await _jsRuntime.InvokeAsync<IJSObjectReference>("import", $"./_content/{PROJECT_NAME}/Components/SourceViewer/sourceViewer.razor.js");
        var markup = await module.InvokeAsync<string>("getOutputSource");

        await module.DisposeAsync();

        return new SourceViewerViewModel(markup);
    }
}

