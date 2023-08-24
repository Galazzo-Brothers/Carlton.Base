namespace Carlton.Core.Components.Flux.Models;

public record MutationCommand(object Sender, Guid CommandID)
{
    public MutationCommand(object sender) : this(sender, Guid.NewGuid())
    { 
    } 

    public virtual void UpdateCommandWithExternalResponse(object response)
    {
        //This method could be overridden 
        //in order to update the command 
        //during interception 
        //ex. this.ID = (<SpecificType>) response.ID
    }
}
