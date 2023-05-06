namespace Carlton.Base.State;

public class ViewModelRequest<TViewModel> : RequestBase, IRequest<TViewModel>
{
    public ViewModelRequest(IDataWrapper sender) : base(sender)
    {
    }
}
