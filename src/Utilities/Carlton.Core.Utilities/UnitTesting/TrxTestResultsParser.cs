using System.Xml.Linq;
using Constants = Carlton.Core.Utilities.UnitTesting.UnitTestingConstants;
namespace Carlton.Core.Utilities.UnitTesting;

/// <summary>
/// Parses TRX test results files.
/// </summary>
public class TrxTestResultsParser : ITestResultsParser
{
	/// <inheritdoc/>
	public TestResultsReport ParseTestResults(string content)
	{
		try
		{
			var document = XDocument.Parse(content);

			var results = document.Elements()
								   .First(x => x.Name.LocalName == Constants.TestRun)
								   .Elements()
								   .Where(x => x.Name.LocalName == Constants.Results)
								   .Elements()
								   .Select(x =>
								   {
									   var resultAttributes = x.Attributes().ToDictionary(attrib => attrib.Name, attrib => attrib.Value);
									   var testName = resultAttributes[Constants.TestName];
									   var testResult = (TestResultOutcomes)Enum.Parse(typeof(TestResultOutcomes), resultAttributes[Constants.Outcome]);
									   var duration = Math.Round(TimeSpan.Parse(resultAttributes[Constants.Duration]).TotalMilliseconds, 2);

									   return new TestResult(testName, testResult, duration);

								   }).ToList();



			return new TestResultsReport(results);
		}
		catch (Exception)
		{
			throw new ArgumentException("Content is not a valid Trx file.");
		}
	}

	/// <inheritdoc/>
	public IDictionary<string, TestResultsReport> ParseTestResultsByGroup(string content)
	{
		var groupedTestResults = new Dictionary<string, List<TestResult>>();

		//Parse the TRX file for a complete list of test results
		//Create a look up of tests to categories
		var allTestResults = ParseTestResults(content).TestResults;
		var categoriesLookup = ParseCategories(content);

		//Find the distinct list of all possible categories
		var distinctCategories = categoriesLookup.SelectMany(x => x.Value).Distinct();

		//Create a grouping of distinct categories to test results
		foreach (var category in distinctCategories)
			groupedTestResults.Add(category, []);

		//Add each test into its respective group
		//a test can appear in multiple groups
		//or a default category if none is specified
		foreach (var testResult in allTestResults)
		{
			var testIsCategorized = categoriesLookup.TryGetValue(testResult.TestName, out var testCategories);

			if (testIsCategorized)
				testCategories.ToList().ForEach(_ => groupedTestResults[_].Add(testResult));
			else
				AddToDefaultGroup(groupedTestResults, testResult);
		}

		//Parse the final response into a <string, TestResultsReport Dictionary
		return groupedTestResults.ToDictionary(x => x.Key, x => new TestResultsReport(x.Value));
	}

	/// <inheritdoc/>
	public IDictionary<string, TestResultsReport> ParseTestResultsByGroup(string content, string groupKey)
	{
		throw new NotImplementedException();
	}

	private static void AddToDefaultGroup(Dictionary<string, List<TestResult>> groupedTestResults, TestResult testResult)
	{
		if (!groupedTestResults.ContainsKey(Constants.Default))
		{
			groupedTestResults.Add(Constants.Default, new List<TestResult>());
		}

		groupedTestResults[Constants.Default].Add(testResult);
	}

	private static Dictionary<string, IEnumerable<string>> ParseCategories(string content)
	{
		var document = XDocument.Parse(content);
		var lookup = new Dictionary<string, IEnumerable<string>>();
		document.Elements()
				.First(x => x.Name.LocalName == Constants.TestRun)
				.Elements()
				.Where(x => x.Name.LocalName == Constants.TestDefinitions)
				.Elements()
				.Where(x => x.Name.LocalName == Constants.UnitTest)
				.ToList()
				.ForEach(x =>
				{
					var name = x.Attribute("name").Value;
					var categoryItems = x.Elements()
						?.FirstOrDefault(el => el.Name.LocalName == Constants.TestCategory)
						?.Elements()
						?.Where(el => el.Name.LocalName == Constants.TestCategoryItem);

					if (categoryItems == null || !categoryItems.Any())
						return;

					var categories = new List<string>();
					foreach (var item in categoryItems)
						categories.Add(item.Attribute(Constants.TestCategory).Value);

					lookup.Add(name, categories);
				});

		return lookup;
	}
}
