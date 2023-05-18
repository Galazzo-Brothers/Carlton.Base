namespace Carlton.Base.TestBed;

public class TestBedViewModelStateFacade : ViewModelStateFacadeBase<TestBedState>
{
    public TestBedViewModelStateFacade(TestBedState state, ILogger<TestBedViewModelStateFacade> logger) : base(state, logger)
    {
    }
}
