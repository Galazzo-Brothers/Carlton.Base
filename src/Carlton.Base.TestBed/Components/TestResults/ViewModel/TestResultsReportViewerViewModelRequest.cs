using Microsoft.AspNetCore.Components;

namespace Carlton.Base.TestBed;

//[ObserveStateEvents<TestBedStateEvents>(TestBedStateEvents.SelectedItem)]
[RefreshViewModel("/UnitTestResults/UnitTestResults.trx")]
public sealed record TestResultsReportViewerViewModelRequest : IViewModelRequest<TestResultsViewModel>
{
}