namespace Carlton.Base.TestBedFramework.Test
{
    public class TrxParserTests
    {
        private readonly string _trxTemplate;
        private readonly ITrxParser _parser;

        public TrxParserTests()
        {
            //Arrange
            _parser = new TrxParser();
            _trxTemplate = XDocument.Load("TrxFiles/TestTrx.xml").ToString();
        }

        [Theory]
        [InlineData(4, 3, 1, TrxTestHelper.StartDate_1, TrxTestHelper.EndDate_1, TrxTestHelper.SummaryDuration_1)]
        [InlineData(7, 5, 2, TrxTestHelper.StartDate_2, TrxTestHelper.EndDate_2, TrxTestHelper.SummaryDuration_2)]
        [InlineData(10, 10, 0, TrxTestHelper.StartDate_3, TrxTestHelper.EndDate_3, TrxTestHelper.SummaryDuration_3)]
        public void TrxParser_ParsingSummarayModel_Success(
            int expectedTotal,
            int expectedSuccess,
            int expectedFailed,
            string startDateString,
            string endDateString,
            double expectedDuration)
        {
            //Arrange
            var content = string.Format(_trxTemplate, expectedTotal, expectedSuccess, expectedFailed, startDateString, endDateString, string.Empty);
            var expected = new TestResultsSummaryModel(expectedTotal, expectedSuccess, expectedFailed, expectedDuration);

            //Act
            var summary = _parser.ParseTrxSummaryContent(content);

            //Assert
            Assert.Equal(expected, summary);
        }

        [Fact]
        public void TrxParser_ParsingSummarayModel_InvalidContent_ThrowsArgumentException()
        {
            //Arrange
            var content = "this is garbage content";

            //Assert
            var ex = Assert.Throws<ArgumentException>(() => _parser.ParseTrxSummaryContent(content));
            Assert.Equal(TrxTestHelper.ExceptionMessage, ex.Message);
        }

        [Theory]
        [MemberData(nameof(TrxTestHelper.GetItems), MemberType = typeof(TrxTestHelper))]
        public void TrxParser_ParsingTestResultsModel_Success((string replacementMarkup, ReadOnlyCollection<TestResultItemModel> expectedOutput) expectedResults)
        {
            //Arrange
            var content = string.Format(_trxTemplate, 3, 1, TrxTestHelper.StartDate_1, TrxTestHelper.EndDate_1, TrxTestHelper.SummaryDuration_1, expectedResults.replacementMarkup);

            //Act
            var actualTestResults = _parser.ParseTrxTestResultsContent(content);

            //Assert
            Assert.Equal(expectedResults.expectedOutput, actualTestResults);
        }

        [Fact]
        public void TrxParser_ParsingTestResultsModel_InvalidContent_ThrowsArgumentException()
        {
            //Arrange
            var content = "this is garbage content";

            //Assert
            var ex = Assert.Throws<ArgumentException>(() => _parser.ParseTrxTestResultsContent(content));
            Assert.Equal(TrxTestHelper.ExceptionMessage, ex.Message);
        }
    }
}