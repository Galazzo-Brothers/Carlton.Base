namespace Carlton.Core.Components.Flux;

public class ViewModelRequest<TViewModel> : RequestBase<TViewModel>
{
    public ViewModelRequest(IDataWrapper sender) : base(sender)
    {
    }
}
