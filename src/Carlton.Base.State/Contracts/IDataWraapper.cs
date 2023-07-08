namespace Carlton.Base.State;

public interface IDataWrapper
{
    public Type WrappedComponentType { get; }
    public object State { get; }
}
