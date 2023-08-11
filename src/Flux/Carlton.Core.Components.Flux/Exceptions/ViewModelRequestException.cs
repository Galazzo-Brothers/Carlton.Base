namespace Carlton.Core.Components.Flux;

[Serializable]
public class ViewModelRequestException<TViewModel> : Exception
{
    private const string ErrorMessage = $"An exception occurred during a request for ViewModel of type {nameof(TViewModel)}";

    public ViewModelQuery Request { get; init; }

    public ViewModelRequestException(ViewModelQuery request, Exception innerException) : base(ErrorMessage, innerException)
    { 
        //if(!request.HasErrored)
        //    request.MarkErrored();

        //Request = request;
    }

    protected ViewModelRequestException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}
