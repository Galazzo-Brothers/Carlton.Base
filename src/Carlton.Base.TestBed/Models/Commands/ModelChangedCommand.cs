namespace Carlton.Base.TestBed;

public sealed record ModelChangedCommand(ComponentParameters ComponentParameters) : ICommand;