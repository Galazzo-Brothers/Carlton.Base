namespace Carlton.Base.State;

public abstract record ComponentEventBase<TViewModel> : IComponentEvent<TViewModel>
{
    public Type ComponentType { get => typeof(TViewModel); }
}
