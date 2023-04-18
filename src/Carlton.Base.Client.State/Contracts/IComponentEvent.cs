namespace Carlton.Base.State;

public interface IComponentEvent<TViewModel>
{
    Type ComponentType { get => typeof(TViewModel); }
}

