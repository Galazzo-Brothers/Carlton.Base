namespace Carlton.Base.State;

public record CommandBase<TServerResponse> : ICommand
{
    public virtual ICommand UpdateWithServerResponse(TServerResponse serverResponse)
    {
        //Empty response from server
        //no transformation required
        return this;
    }
}
