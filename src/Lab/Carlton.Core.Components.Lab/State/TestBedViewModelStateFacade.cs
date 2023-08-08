namespace Carlton.Core.Components.Lab;

public class TestBedViewModelStateFacade : ViewModelStateFacade<LabState>
{
    public TestBedViewModelStateFacade(LabState state, ILogger<TestBedViewModelStateFacade> logger) : base(state, logger)
    {
    }
}
