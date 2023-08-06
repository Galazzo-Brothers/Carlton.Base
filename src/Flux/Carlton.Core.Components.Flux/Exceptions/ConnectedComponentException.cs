namespace Carlton.Core.Components.Flux;

[Serializable]
public class ConnectedComponentException<TViewModel, TStateEvents> : Exception
    where TStateEvents : Enum
{
    private static readonly string HandlingStateEventErrorMessage = $"An Error occurred while handling a StateEvent in the ConnectedComponent of type {typeof(TViewModel).Name}";
    private static readonly string SettingComponentParametersMessage = $"An Error occurred while setting the wrapped component parameters in the ConnectedComponent of type {typeof(TViewModel).Name}";

    public Type WrappedComponentType { get; init; }
    public IEnumerable<TStateEvents> ObservableStateEvents { get; init; }
    public TViewModel ViewModel { get; init; }


    private ConnectedComponentException(string message, ConnectedWrapper<TViewModel, TStateEvents>  wrapper,  Exception innerException)
        : base(message, innerException)
    {
        WrappedComponentType = wrapper.WrappedComponentType;
        ObservableStateEvents = wrapper.ObserveableStateEvents;
        ViewModel = wrapper.ViewModel;
    }

    protected ConnectedComponentException(SerializationInfo info, StreamingContext context)
    : base(info, context)
    {
    }

    public static ConnectedComponentException<TViewModel, TStateEvents> CreateStateEventException(ConnectedWrapper<TViewModel, TStateEvents> wrapper, Exception innerException)
    {
        return new ConnectedComponentException<TViewModel, TStateEvents>(HandlingStateEventErrorMessage, wrapper, innerException);
    }

    public static ConnectedComponentException<TViewModel, TStateEvents> CreateSettingComponentParametersException(ConnectedWrapper<TViewModel, TStateEvents> wrapper, Exception innerException)
    {
        return new ConnectedComponentException<TViewModel, TStateEvents>(SettingComponentParametersMessage, wrapper, innerException);
    }
}
