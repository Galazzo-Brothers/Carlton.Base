using Bunit;
using Carlton.Core.Components.Lab.Models;
using Carlton.Core.Utilities.UnitTesting;

namespace Carlton.Core.Components.Lab.Test;

public class ConnectedTestResultsComponentTests : TestContext
{
    [Fact]
    public void ConnectedTestResultsViewerComponentRendersCorrectly()
    {
        //Arrange
        var vm = new TestResultsViewModel(new List<TestResult>
        {
            new TestResult("Test 1", TestResultOutcomes.Passed, 1.22),
            new TestResult("Test 2", TestResultOutcomes.Passed, 1.55),
            new TestResult("Test 3", TestResultOutcomes.Failed, 15.22),
        });

        //Act
        var cut = RenderComponent<ConnectedTestResultsViewer>(parameters => parameters
                .Add(p => p.ViewModel, vm));

        //Assert
        cut.MarkupMatches(@"
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
            <span class=""test-passed-icon mdi mdi-18px mdi-check-circle""></span>
            <span>Passed</span>
            </div>
        </div>
        <div class=""test-name-column"">Test 1</div>
        <div class=""test-duration-column"">1220</div>
        </div>
    </div>
    <div class=""item-row table-row"">
        <div class=""test-row"">
        <div class=""test-outcome-column"">
            <div class=""test-outcome-icon"">
            <span class=""test-passed-icon mdi mdi-18px mdi-check-circle""></span>
            <span>Passed</span>
            </div>
        </div>
        <div class=""test-name-column"">Test 2</div>
        <div class=""test-duration-column"">1550</div>
        </div>
    </div>
    <div class=""item-row table-row"">
        <div class=""test-row"">
        <div class=""test-outcome-column"">
            <div class=""test-outcome-item"">
            <span class=""test-failed-icon mdi mdi-18px mdi-close-circle""></span>
            <span>Failed</span>
            </div>
        </div>
        <div class=""test-name-column"">Test 3</div>
        <div class=""test-duration-column"">15220</div>
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
}
