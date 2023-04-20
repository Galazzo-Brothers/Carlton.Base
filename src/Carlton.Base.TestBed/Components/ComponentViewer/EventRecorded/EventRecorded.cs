namespace Carlton.Base.TestBed;
public record EventRecorded(string Name, object Obj) : ComponentEventBase<ComponentViewerViewModel>
{
    public EventRecorded(string name) : this(name, null) 
        => Name = name;
}