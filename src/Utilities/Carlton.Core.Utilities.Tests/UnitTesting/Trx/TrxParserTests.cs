namespace Carlton.Core.Utilities.Test.UnitTesting.Trx;

public class TrxParserTests
{
    private readonly ITestResultsParser _parser = new TrxTestResultsParser();


    [Theory]
    [ClassData(typeof(TrxParserTestData))]
    public void TrxParser_ParsingTestResults_Success(string content, TestResultsReport expectedOutput)
    {
        //Act
        var actual = _parser.ParseTestResults(content);

        //Assert
        Assert.Equal(expectedOutput.Summary, actual.Summary);
        Assert.Equal(expectedOutput.TestResults, actual.TestResults);
    }

    [Theory]
    [ClassData(typeof(TrxParserGroupedTestData))]
    public void TrxParser_ParsingTestByGroupResults_Success(string content, IDictionary<string, TestResultsReport> expected)
    {
        //Act
        var actual = _parser.ParseTestResultsByGroup(content);

        //Assert
        Assert.Equal(expected.Count, actual.Count);
        Assert.All(actual, item =>
        {
            Assert.Equal(expected[item.Key].Summary, actual[item.Key].Summary);
            Assert.Equal(expected[item.Key].TestResults, actual[item.Key].TestResults);
        });
    }
}
