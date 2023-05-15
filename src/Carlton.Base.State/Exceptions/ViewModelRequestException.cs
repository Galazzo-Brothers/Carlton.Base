namespace Carlton.Base.State;

[Serializable]
public class ViewModelRequestException<TViewModel> : Exception
{
    private const string ErrorMessage = $"An exception occurred during a request for ViewModel of type {nameof(TViewModel)}";

    public ViewModelRequest<TViewModel> Request { get; init; }

    public ViewModelRequestException(ViewModelRequest<TViewModel> request, Exception innerException) : base(ErrorMessage, innerException)
    {
        request.MarkErrored();
        Request = request;
    }

    protected ViewModelRequestException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}
