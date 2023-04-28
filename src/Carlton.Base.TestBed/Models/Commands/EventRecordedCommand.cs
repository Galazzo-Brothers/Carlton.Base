namespace Carlton.Base.TestBed;
public sealed record EventRecordedCommand(string Name, object Obj) : ICommand
{
    public EventRecordedCommand(string name) : this(name, null)
        => Name = name;
}


