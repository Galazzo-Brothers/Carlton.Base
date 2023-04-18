namespace Carlton.Base.Components.Test;

public static class CardTestHelper
{
    public const string CardMarkup = 
    @"
<div class=""card"" b-g3swmy425k>
    <div class=""content"" b-g3swmy425k>
        <div class=""title-content"" b-g3swmy425k>
            <span class=""card-title"" b-g3swmy425k>Test Title</span>
            <div class=""status-icon"" b-g3swmy425k>Test ActionBar Content</div>
        </div>
        <div class=""header-content"" b-g3swmy425k>
            <span>Test Header</span>
        </div>
        <div class=""primary-content"" b-g3swmy425k>
            <span>Test Primary Content</span>
        </div>
    </div>
</div>";

    public const string CountCardMarkup =
    @"
<div class=""count-card accent4"" b-xj8ojuw516>
    <div class=""content"" b-xj8ojuw516>
        <div class=""count-icon mdi mdi-48px mdi-delete"" b-xj8ojuw516></div>
        <span class=""count-message"" b-xj8ojuw516>5 Items Tracked by this component</span>
    </div>
</div>";

    public const string ListCardMarkup =
    @"
<div class=""card"" b-g3swmy425k>
  <div class=""content"" b-g3swmy425k>
    <div class=""title-content"" b-g3swmy425k>
      <span class=""card-title"" b-g3swmy425k>List Card Title</span>
      <div class=""status-icon"" b-g3swmy425k>
        <div class=""dropdown-menu"" blazor:onclick=""1"" b-e7te7pxji7>
          <div class=""menu"" b-e7te7pxji7>
            <i class=""mdi mdi-24px mdi-dots-vertical"" b-m40yy2q1d3></i>
          </div>
          <div class=""dropdown "" style=""--dropdown-left:10px;--dropdown-top:10px;--dropdown-top-mobile:10px;"" b-e7te7pxji7>
            <ul b-e7te7pxji7>
                 <div class=""header"" b-e7te7pxji7></div>
            </ul>
          </div>
        </div>
      </div>
    </div>
    <div class=""header-content"" b-g3swmy425k>
      <span>Header Content</span>
    </div>
    <div class=""primary-content"" b-g3swmy425k>
      <div class=""sub-title"" b-22crlgp3bk>Some Test Subtitle</div>
      <ul b-22crlgp3bk>
        <li b-22crlgp3bk>
          <span>1</span>
        </li>
        <li b-22crlgp3bk>
          <span>2</span>
        </li>
        <li b-22crlgp3bk>
          <span>3</span>
        </li>
      </ul>
    </div>
  </div>
</div>";

    public static IEnumerable<object[]> GetItems()
    {
        yield return new object[]
           {
               new List<int> { 1, 2, 3 }.AsReadOnly()
           };
        yield return new object[]
           {
              new List<int> { 1, 2, 3, 10, 15 }.AsReadOnly()
            };
        yield return new object[]
            {
                new List<int> { 7 }.AsReadOnly()
            };
    }

    public static readonly IReadOnlyCollection<int> Items = new List<int> { 1, 2, 3 };
}
