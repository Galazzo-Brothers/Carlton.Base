using AutoFixture.AutoNSubstitute;
using AutoFixture;
using AutoFixture.Xunit2;
using Carlton.Core.Foundation.Tests;
namespace Carlton.Core.Foundation.Test;

public class AutoNSubstituteDataAttribute : AutoDataAttribute
{
    public AutoNSubstituteDataAttribute()
            : base(() => new Fixture()
                .Customize(new AutoNSubstituteCustomization())
                .Customize(new HttpClientCustomization()))
    {
    }
}
