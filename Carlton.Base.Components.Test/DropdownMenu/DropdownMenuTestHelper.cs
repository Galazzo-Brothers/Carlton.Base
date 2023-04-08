namespace Carlton.Base.Components.Test;

public static class DropdownMenuTestHelper
{
    public readonly static string DropDownMenuMarkup = @"
<div class=""dropdown-menu"" blazor:onclick=""1"" b-e7te7pxji7>
  <div class=""menu"" b-e7te7pxji7><i class='False'></i></div>
  <div class=""dropdown "" style=""--dropdown-left:50px;--dropdown-top:75px;--dropdown-top-mobile:37px;"" b-e7te7pxji7>
    <ul b-e7te7pxji7>
      <div class=""header"" b-e7te7pxji7>  
        <span>Header</span>
      </div>
      <li b-e7te7pxji7>
        <span class='item'>Item 1</span>
      </li>
      <li b-e7te7pxji7>
        <span class='item'>Item 2</span>
      </li>
      <li b-e7te7pxji7>
        <span class='item'>Item 3</span>
      </li>
    </ul>
  </div>
</div>";

    public readonly static ReadOnlyCollection<DropdownMenuItem<int>> DropdownMenuItems = new List<DropdownMenuItem<int>>
    {
        new DropdownMenuItem<int>("Item 1", 1, "icon-1", 1),
        new DropdownMenuItem<int>("Item 2", 2, "icon-2", 2),
        new DropdownMenuItem<int>("Item 3", 3, "icon-3", 3)
    }.AsReadOnly();

    public readonly static DropdownMenuStyle Style = new(50, 75, 37);

    public static IEnumerable<object[]> GetItems()
    {
        yield return new object[]
        {
                new List<DropdownMenuItem<int>>()
                {
                    new DropdownMenuItem<int>("Item 1", 1, "icon-1", 1)
                }.AsReadOnly()
        };
        yield return new object[]
        {
                new List<DropdownMenuItem<int>>()
                {
                    new DropdownMenuItem<int>("Item 1", 1, "icon-1", 1),
                    new DropdownMenuItem<int>("Item 2", 2, "icon-2", 2)
                }.AsReadOnly()
        };
        yield return new object[]
        {
                new List<DropdownMenuItem<int>>()
                {
                    new DropdownMenuItem<int>("Item 1", 1, "icon-1", 1),
                    new DropdownMenuItem<int>("Item 2", 2, "icon-2", 2),
                    new DropdownMenuItem<int>("Item 3", 3, "icon-3", 3)
                }.AsReadOnly()
        };
    }
}
