namespace Carlton.Base.TestBed;

public record EventConsoleViewModel(IEnumerable<(string Name, object Obj)> ComponentEvents);