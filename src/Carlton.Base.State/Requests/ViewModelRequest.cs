namespace Carlton.Base.State;

public class ViewModelRequest<TViewModel> : ComponentRequestBase, IRequest<TViewModel>
{
    public ViewModelRequest(object sender) : base(sender)
    {
    }
}
