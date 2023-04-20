namespace Carlton.Base.TestBed;

public sealed record ModelChanged(object ComponentParameters) : ComponentEventBase<ModelViewerViewModel>;