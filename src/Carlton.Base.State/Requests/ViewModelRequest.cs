namespace Carlton.Base.State;

public class ViewModelRequest<TViewModel> : RequestBase, IRequest<TViewModel>
{
    public ViewModelRequest(object sender) : base(sender)
    {
    }
}
