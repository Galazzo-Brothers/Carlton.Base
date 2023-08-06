namespace Carlton.Core.Components.Flux;

public interface IDataWrapper
{
    public Type WrappedComponentType { get; }
    public object State { get; }
}
