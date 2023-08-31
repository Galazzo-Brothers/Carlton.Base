using AutoFixture.AutoMoq;
using FluentValidation;
using Microsoft.JSInterop;
using System.Text.Json;

namespace Carlton.Core.Components.Flux.Test.Common;

public static class TestDataGenerator
{
    public static IEnumerable<object[]> GetViewModelData()
    {
        var fixture = new Fixture().Customize(new AutoMoqCustomization());

        return new List<object[]>
        {
            new object[] { fixture.Create<TestViewModel1>()  },
            new object[] { fixture.Create<TestViewModel2>() },
        };
    }

    public static IEnumerable<object[]> GetCommandData()
    {
        var fixture = new Fixture().Customize(new AutoMoqCustomization());

        return new List<object[]>
        {
            new object[] { fixture.Create<TestCommand1>() },
            new object[] { fixture.Create<TestCommand2>() },
        };
    }

    public static IEnumerable<object[]> GetJsCallersData()
    {
        var testData = new List<object[]>
        {
            new object[] { new JsRefreshCaller()  },
            new object[] { new JsRefreshCaller2() },
        };

        return testData;
    }

    public static IEnumerable<object[]> GetViewModelExceptionData()
    {
        var testData = new List<object[]>
        {
            new object[] { new JsonException(), LogEvents.ViewModel_JSON_ErrorMsg  },
            new object[] { new HttpRequestException(), LogEvents.ViewModel_HTTP_ErrorMsg },
            new object[] { new JSException("There was a JS error."), LogEvents.ViewModel_JSInterop_ErrorMsg },
            new object[] { new ValidationException("There was a validation error."), LogEvents.ViewModel_Validation_ErrorMsg },
            new object[] { new Exception(), LogEvents.ViewModel_Unhandled_ErrorMsg }
        };

        return testData;
    }

    public static IEnumerable<object[]> GetMutationExceptionData()
    {
        var testData = new List<object[]>
        {
            new object[] { new JsonException(), LogEvents.Mutation_JSON_ErrorMsg  },
            new object[] { new HttpRequestException(), LogEvents.Mutation_HTTP_ErrorMsg },
            new object[] { new JSException("There was a JS error."), LogEvents.Mutation_JSInterop_ErrorMsg },
            new object[] { new ValidationException("There was a validation error."), LogEvents.Mutation_Validation_ErrorMsg },
            new object[] { new Exception(), LogEvents.Mutation_Unhandled_ErrorMsg }
        };

        return testData;
    }
}
