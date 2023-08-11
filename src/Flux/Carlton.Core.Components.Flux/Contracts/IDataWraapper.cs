namespace Carlton.Core.Components.Flux.Contracts;

public interface IDataWrapper
{
    public Type WrappedComponentType { get; }
    public object State { get; }
}
