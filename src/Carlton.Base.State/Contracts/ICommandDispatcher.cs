namespace Carlton.Base.State
{
    public interface ICommandDispatcher
    {
        public Task<Unit> Dispatch<TCommand>(CommandRequest<TCommand> request, CancellationToken cancellationToken)
            where TCommand : ICommand;
    }
}