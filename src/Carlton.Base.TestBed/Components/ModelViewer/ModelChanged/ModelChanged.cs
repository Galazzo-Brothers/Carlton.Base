namespace Carlton.Base.TestBedFramework;

public record ModelChanged(object ComponentViewModel) : IComponentEvent<ModelViewerViewModel>;