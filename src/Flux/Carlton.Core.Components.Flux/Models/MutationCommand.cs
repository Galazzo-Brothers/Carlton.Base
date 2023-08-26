namespace Carlton.Core.Components.Flux.Models;

public record class MutationCommand
{
    public object Sender { get; }
    public Guid CommandID { get; }
    public int SourceSystemID { get; private set; }

    public MutationCommand(object sender)
     : this(sender, -1)
    {
    }

    public MutationCommand(object sender, int sourceSystemID)
    {
        Sender = sender;
        SourceSystemID = sourceSystemID;
        CommandID = Guid.NewGuid();
    }

    public virtual void UpdateCommandWithExternalResponse(MutationCommand command)
    {
        //This method could be overridden 
        //in order to update the command 
        //during interception 
        //ex. this.ID = (<SpecificType>) response.ID
        SourceSystemID = command.SourceSystemID;
    }
}
