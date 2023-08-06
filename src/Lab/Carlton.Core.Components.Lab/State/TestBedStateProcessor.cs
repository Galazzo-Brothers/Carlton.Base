namespace Carlton.Core.Components.Lab;

public class TestBedStateProcessor : StateProcessorBase<TestBedStateMutationCommands>
{
    public TestBedStateProcessor(TestBedStateMutationCommands state) : base(state)
    {
    }

    public override async Task ProcessCommand<TCommand>(object sender, TCommand command)
    {
        await State.ProcessCommand(sender, (dynamic)command);
    }
}

