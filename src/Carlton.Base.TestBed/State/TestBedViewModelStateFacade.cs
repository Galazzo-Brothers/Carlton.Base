namespace Carlton.Base.TestBed;

public class TestBedViewModelStateFacade : ViewModelStateFacade<TestBedState>
{
    public TestBedViewModelStateFacade(TestBedState state, ILogger<TestBedViewModelStateFacade> logger) : base(state, logger)
    {
    }
}
