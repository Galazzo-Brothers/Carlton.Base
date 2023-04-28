
namespace Carlton.Base.State;

public interface ICommandProcessor
{
    public Task ProcessCommand<TCommand>(object sender, TCommand command)
        where TCommand : ICommand;
}
