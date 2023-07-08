namespace Carlton.Base.Infrastructure.Test.UnitTesting.XUnit;

public class XUnitParserTests
{
    private readonly ITestResultsParser _parser;

    public XUnitParserTests()
    {
        //Arrange
        _parser = new XUnitTestResultsParser();
    }

    [Theory]
    [ClassData(typeof(XUnitParserTestData))]
    public void XUnitParser_ParsingTestReport_Success(string content, TestResultsReport expectedResult)
    {
        //Act
        var actual = _parser.ParseTestResults(content);

        //Assert
        Assert.Equal(expectedResult.TestResults, actual.TestResults);
        Assert.Equal(expectedResult.Summary, actual.Summary);
    }

    [Theory]
    [ClassData(typeof(XUnitParserGroupedTestData))]
    public void XUnitParser_ParsingTestReportByGroup_Success(string content, IDictionary<string, TestResultsReport> expectedResults)
    {
        //Act
        var actual = _parser.ParseTestResultsByGroup(content, "Category");

        //Assert
        Assert.Equal(expectedResults.Count, actual.Count);
        Assert.All(actual, item =>
        {
            Assert.Equal(expectedResults[item.Key].Summary, actual[item.Key].Summary);
            Assert.Equal(expectedResults[item.Key].TestResults, actual[item.Key].TestResults);
        });
    }


    [Fact]
    public void XUnitParser_ParsingInvalidFile_ThrowsInvalidOperationException()
    {
        //Arrange
        var expectedMessage = "Content is not a valid XUnit test results file.";

        //Act
        var ex = Assert.Throws<ArgumentException>(() => _parser.ParseTestResults("Some junk text"));

        //Assert
        Assert.Equal(expectedMessage, ex.Message);
    }

    [Fact]
    public void XUnitParser_ParsingGroupsInvalidFile_ThrowsInvalidOperationException()
    {
        //Arrange
        var expectedMessage = "Content is not a valid XUnit test results file.";

        //Act
        var ex = Assert.Throws<ArgumentException>(() => _parser.ParseTestResultsByGroup("Some junk text", "does not matter"));

        //Assert
        Assert.Equal(expectedMessage, ex.Message);
    }
}
