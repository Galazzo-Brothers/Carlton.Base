namespace Carlton.Base.State;

public class ViewModelRequest<TViewModel> : RequestBase<TViewModel>
{
    public ViewModelRequest(IDataWrapper sender) : base(sender)
    {
    }
}
