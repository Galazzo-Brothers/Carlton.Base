using AutoFixture;
using Bunit;
using Carlton.Core.Utilities.UnitTesting;

namespace Carlton.Core.Components.Lab.Test.ComponentTests;

public class ConnectedTestResultsComponentTests : TestContext
{
    private readonly IFixture _fixture;

    public ConnectedTestResultsComponentTests()
    {
        _fixture = new Fixture();
    }

    [Fact]
    public void ConnectedTestResultsViewerComponentRendersCorrectly()
    {
        //Arrange
        var testResults = _fixture.CreateMany<TestResult>(3).ToList();
        var vm = new TestResultsViewModel(testResults);

        //Act
        var cut = RenderComponent<ConnectedTestResultsViewer>(parameters => parameters
                .Add(p => p.ViewModel, vm));

        //Assert
        cut.MarkupMatches(@$"
<div class=""test-results-viewer"">
<div class=""main-container"">
    <div class=""table-container"">
    <div class=""header-row table-row"">
        <div class=""table-header-row"">
        <div class=""header-row-item row-item ascending heading-0"">
            <span class=""heading-text"">Result</span>
            <div class=""sort-arrows"">
            <span class=""arrow-ascending mdi mdi-arrow-up""></span>
            <span class=""arrow-descending mdi mdi-arrow-down""></span>
            </div>
        </div>
        <div class=""header-row-item row-item ascending heading-1"">
            <span class=""heading-text"">Test Name</span>
            <div class=""sort-arrows"">
            <span class=""arrow-ascending mdi mdi-arrow-up""></span>
            <span class=""arrow-descending mdi mdi-arrow-down""></span>
            </div>
        </div>
        <div class=""header-row-item row-item ascending heading-2"">
            <span class=""heading-text"">Duration (Milliseconds )</span>
            <div class=""sort-arrows"">
            <span class=""arrow-ascending mdi mdi-arrow-up""></span>
            <span class=""arrow-descending mdi mdi-arrow-down""></span>
            </div>
        </div>
        </div>
    </div>
    <div class=""item-row table-row"">
        <div class=""test-row"">
        <div class=""test-outcome-column"">
            <div class=""test-outcome-icon"">
            <span class=""test-{testResults[0].TestResultOutcome.ToString().ToLower()}-icon mdi mdi-18px mdi-{GetTestIcon(testResults[0].TestResultOutcome)}-circle""></span>
            <span>{testResults[0].TestResultOutcome}</span>
            </div>
        </div>
        <div class=""test-name-column"">{testResults[0].TestName}</div>
        <div class=""test-duration-column"">{testResults[0].TestDuration}</div>
        </div>
    </div>
    <div class=""item-row table-row"">
        <div class=""test-row"">
        <div class=""test-outcome-column"">
            <div class=""test-outcome-icon"">
            <span class=""test-{testResults[1].TestResultOutcome.ToString().ToLower()}-icon mdi mdi-18px mdi-{GetTestIcon(testResults[1].TestResultOutcome)}-circle""></span>
            <span>{testResults[1].TestResultOutcome}</span>
            </div>
        </div>
        <div class=""test-name-column"">{testResults[1].TestName}</div>
        <div class=""test-duration-column"">{testResults[1].TestDuration}</div>
        </div>
    </div>
    <div class=""item-row table-row"">
        <div class=""test-row"">
        <div class=""test-outcome-column"">
            <div class=""test-outcome-icon"">
            <span class=""test-{testResults[2].TestResultOutcome.ToString().ToLower()}-icon mdi mdi-18px mdi-{GetTestIcon(testResults[2].TestResultOutcome)}-circle""></span>
            <span>{testResults[2].TestResultOutcome}</span>
            </div>
        </div>
        <div class=""test-name-column"">{testResults[2].TestName}</div>
        <div class=""test-duration-column"">{testResults[2].TestDuration}</div>
        </div>
    </div>
    <div class=""pagination-row table-row"">
        <div class=""pagination-row-item"">
        <div class=""rows-per-page"">
            <span class=""rows-per-page-label"">Rows Per Page</span>
            <div class=""select"">
            <input readonly="""" placeholder="" "" value=""5"">
            <div class=""label""></div>
            <div class=""options"">
                <div class=""option"">5</div>
                <div class=""option"">10</div>
                <div class=""option"">25</div>
            </div>
            </div>
        </div>
        <div class=""page-number"">
            <span class=""pagination-label"">1-3 of 3</span>
        </div>
        <div class=""page-chevrons"">
            <span class=""mdi mdi-18px mdi-page-first disabled""></span>
            <span class=""mdi mdi-18px mdi-chevron-left disabled""></span>
            <span class=""mdi mdi-18px mdi-chevron-right disabled""></span>
            <span class=""mdi mdi-18px mdi-page-last disabled""></span>
        </div>
        </div>
    </div>
    </div>
</div>
</div>");
    }

    private static string GetTestIcon(TestResultOutcomes outcome)
    {
        return outcome switch
        {
            TestResultOutcomes.Passed => "check",
            TestResultOutcomes.Failed => "close",
            _ => "help",
        };
    }
}
