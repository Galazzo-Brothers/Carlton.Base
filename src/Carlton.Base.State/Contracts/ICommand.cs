namespace Carlton.Base.State;

public interface ICommand
{
    public string CommandName => GetType().GetDisplayName();
}