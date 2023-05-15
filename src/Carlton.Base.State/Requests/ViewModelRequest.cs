namespace Carlton.Base.State;

public class ViewModelRequest<TViewModel> : RequestBase, IRequest<TViewModel>
{
    public override string RequestName => $"{typeof(ViewModelRequest<>).GetDisplayName()}_{nameof(TViewModel)}";

    public ViewModelRequest(IDataWrapper sender) : base(sender)
    {
    }
}
