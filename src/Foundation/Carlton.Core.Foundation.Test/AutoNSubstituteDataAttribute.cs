using AutoFixture.AutoNSubstitute;
using AutoFixture;
using AutoFixture.Xunit2;
namespace Carlton.Core.Foundation.Test;

public class AutoNSubstituteDataAttribute : AutoDataAttribute
{
    public AutoNSubstituteDataAttribute()
            : base(() => new Fixture()
                .Customize(new AutoNSubstituteCustomization()))
    {
    }
}
