namespace Carlton.Base.State;

public interface IComponentEvent<TViewModel>
{
    public Type ComponentType { get => typeof(TViewModel); }
}

