namespace Carlton.Base.Components.Test;

public static class SelectTestHelper
{
    public const string SelectMarkup = 
    @"
 <div class=""select"" b-b4t7b28hd7><input readonly placeholder="" "" value=""Option 2"" b-b4t7b28hd7 />
    <div class=""label"" b-b4t7b28hd7>Test Select</div>
    <div class=""options"" b-b4t7b28hd7>
        <div class=""option"" blazor:onclick=""1"" b-b4t7b28hd7>Option 1</div>
        <div class=""option"" blazor:onclick=""2"" b-b4t7b28hd7>Option 2</div>
        <div class=""option"" blazor:onclick=""3"" b-b4t7b28hd7>Option 3</div>
    </div>
</div>";

    public static readonly IReadOnlyDictionary<string, int> Options = new Dictionary<string, int>
    {
        { "Option 1", 1 },
        { "Option 2", 2 },
        { "Option 3", 3 }
    };

    public static IEnumerable<object[]> GetOptions()
    {
        yield return new object[]
           {
                new Dictionary<string, int>
                {
                    { "Item 1", 1 }
                }.AsReadOnly()
           };
        yield return new object[]
           {
               new Dictionary<string, int>
                {
                    { "Item 1", 1 },
                    { "Item 2", 2 }
                }.AsReadOnly()
           };
        yield return new object[]
            {
                new Dictionary<string, int>
                {
                    { "Item 1", 1 },
                    { "Item 2", 2 },
                    { "Item 3", 3 }
                }.AsReadOnly()
            };
    }
}
