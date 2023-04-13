namespace Carlton.Base.TestBedFramework;

public record ModelChanged : ComponentEventBase<ModelViewerViewModel>
{
    public object ComponentViewModel { get; }

    public ModelChanged(ICarltonComponent<ModelViewerViewModel> sender, object componentViewModel)
        : base(sender) => ComponentViewModel = componentViewModel;
    
}