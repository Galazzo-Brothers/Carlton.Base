namespace Carlton.Base.State;

[Serializable]
public class DataComponentException<TViewModel, TStateEvents> : Exception
    where TStateEvents : Enum
{
    private static readonly string HandlingStateEventErrorMessage = $"An Error occurred while handling a StateEvent in the DataComponent of type {typeof(TViewModel).Name}";
    private static readonly string SettingComponentParametersMessage = $"An Error occurred while setting the wrapped component parameters in the DataComponent of type {typeof(TViewModel).Name}";

    public Type WrappedComponentType { get; init; }
    public IEnumerable<TStateEvents> ObservableStateEvents { get; init; }
    public TViewModel ViewModel { get; init; }


    private DataComponentException(string message, DataWrapper<TViewModel, TStateEvents>  wrapper,  Exception innerException)
        : base(message, innerException)
    {
        WrappedComponentType = wrapper.WrappedComponentType;
        ObservableStateEvents = wrapper.ObserveableStateEvents;
        ViewModel = wrapper.ViewModel;
    }

    protected DataComponentException(SerializationInfo info, StreamingContext context)
    : base(info, context)
    {
    }

    public static DataComponentException<TViewModel, TStateEvents> CreateStateEventException(DataWrapper<TViewModel, TStateEvents> wrapper, Exception innerException)
    {
        return new DataComponentException<TViewModel, TStateEvents>(HandlingStateEventErrorMessage, wrapper, innerException);
    }

    public static DataComponentException<TViewModel, TStateEvents> CreateSettingComponentParametersException(DataWrapper<TViewModel, TStateEvents> wrapper, Exception innerException)
    {
        return new DataComponentException<TViewModel, TStateEvents>(SettingComponentParametersMessage, wrapper, innerException);
    }
}
