namespace Carlton.Core.Components.Library.Tests;

public static class AccordionSelectTestHelper
{
    public const string AccordionSelectNoItemsMarkup =
    @"
    <div class=""accordion-select"" b-6835cu0hu3>
        <div class=""content"" b-6835cu0hu3>
                <div class=""accordion-header"" blazor:onclick=""1"" b-6835cu0hu3>
                <span class=""accordion-icon-btn mdi mdi-icon mdi-24px mdi-plus-box-outline"" b-6835cu0hu3></span>
                <span class=""item-group-name"" b-6835cu0hu3>AccordionSelect Title</span>
        </div>        
        <div class=""item-container collapsed"" b-6835cu0hu3></div>
        </div>
    </div>";

    public const string AccordionSelectWithItemsMarkup =
    @"
    <div class=""accordion-select"" b-6835cu0hu3>
      <div class=""content"" b-6835cu0hu3>
        <div class=""accordion-header"" blazor:onclick=""1"" b-6835cu0hu3>
          <span class=""accordion-icon-btn mdi mdi-icon mdi-24px mdi-minus-box-outline"" b-6835cu0hu3></span>
          <span class=""item-group-name"" b-6835cu0hu3>AccordionSelect Title</span>
        </div>
        <div class=""item-container"" b-6835cu0hu3>
          <div class=""item"" blazor:onclick=""2"" b-6835cu0hu3>
            <span class=""icon mdi mdi-icon mdi-12px mdi-bookmark"" b-6835cu0hu3></span>
            <span class=""item-name"" b-6835cu0hu3>Item 1</span>
          </div>
          <div class=""item"" blazor:onclick=""3"" b-6835cu0hu3>
            <span class=""icon mdi mdi-icon mdi-12px mdi-bookmark"" b-6835cu0hu3></span>
            <span class=""item-name"" b-6835cu0hu3>Item 2</span>
          </div>
          <div class=""item"" blazor:onclick=""4"" b-6835cu0hu3>
            <span class=""icon mdi mdi-icon mdi-12px mdi-bookmark"" b-6835cu0hu3></span>
            <span class=""item-name"" b-6835cu0hu3>Item 3</span>
          </div>
        </div>
      </div>
    </div>";

    public const string AccordionSelectGroupMarkup =
    @"
  <div class=""accordion-select-group"" b-pnd1og41cd>
    <div class=""accordion-select"" b-6835cu0hu3>
        <div class=""content"" b-6835cu0hu3>
            <div class=""accordion-header"" blazor:onclick=""1"" b-6835cu0hu3>
                <span class=""accordion-icon-btn mdi mdi-icon mdi-24px mdi-minus-box-outline"" b-6835cu0hu3></span>
                <span class=""item-group-name"" b-6835cu0hu3>Group 1</span>
            </div>
            <div class=""item-container"" b-6835cu0hu3>
                <div class=""item"" blazor:onclick=""2"" b-6835cu0hu3>
                    <span class=""icon mdi mdi-icon mdi-12px mdi-bookmark"" b-6835cu0hu3></span>
                    <span class=""item-name"" b-6835cu0hu3>Item 1</span>
                </div>
                <div class=""item"" blazor:onclick=""3"" b-6835cu0hu3>
                    <span class=""icon mdi mdi-icon mdi-12px mdi-bookmark"" b-6835cu0hu3></span>
                    <span class=""item-name"" b-6835cu0hu3>Item 2</span>
                </div>
            </div>
        </div>
    </div>
    <div class=""accordion-select"" b-6835cu0hu3>
        <div class=""content"" b-6835cu0hu3>
            <div class=""accordion-header"" blazor:onclick=""4"" b-6835cu0hu3>
                <span class=""accordion-icon-btn mdi mdi-icon mdi-24px mdi-plus-box-outline"" b-6835cu0hu3></span>
                <span class=""item-group-name"" b-6835cu0hu3>Group 2</span>
            </div>
            <div class=""item-container collapsed"" b-6835cu0hu3>
                <div class=""item"" blazor:onclick=""5"" b-6835cu0hu3>
                    <span class=""icon mdi mdi-icon mdi-12px mdi-bookmark"" b-6835cu0hu3></span>
                    <span class=""item-name"" b-6835cu0hu3>Item 3</span>
                </div>
                <div class=""item"" blazor:onclick=""6"" b-6835cu0hu3>
                    <span class=""icon mdi mdi-icon mdi-12px mdi-bookmark"" b-6835cu0hu3></span>
                    <span class=""item-name"" b-6835cu0hu3>Item 4</span>
                </div>
            </div>
        </div>
    </div>
  </div>";


    public static IEnumerable<object[]> GetItems()
    {
        yield return new object[]
           {
                new List<SelectItem<int>>()
                {
                    new SelectItem<int>("Item 1", 0, 1),
                }.AsReadOnly()
           };
        yield return new object[]
           {
                new List<SelectItem<int>>()
                {
                    new SelectItem<int>("Item 1", 0, 1),
                    new SelectItem<int>("Item 2", 1, 2),
                }.AsReadOnly()
           };
        yield return new object[]
            {
                new List<SelectItem<int>>()
                {
                    new SelectItem<int>("Item 1", 0, 1),
                    new SelectItem<int>("Item 2", 1, 2),
                    new SelectItem<int>("Item 3", 2, 3)
                }.AsReadOnly()
            };
    }

    public static IEnumerable<object[]> GetGroups()
    {
        yield return new object[]
           {
               new List<SelectGroup<int>>
               {
                    new SelectGroup<int>("Group 1", 0,
                        new List<SelectItem<int>>
                        {
                            new SelectItem<int>("Item 1", 0, 1),
                            new SelectItem<int>("Item 2", 1, 2)
                        })
               }.AsReadOnly()
           };
        yield return new object[]
           {
               new List<SelectGroup<int>>
               {
                 new SelectGroup<int>("Group 1", 0,
                    new List<SelectItem<int>>
                    {
                        new SelectItem<int>("Item 1", 0, 1),
                        new SelectItem<int>("Item 2", 1, 2)
                    }),
                 new SelectGroup<int>("Group 2", 1,
                    new List<SelectItem<int>>
                    {
                        new SelectItem<int>("Item 3", 0, 3),
                        new SelectItem<int>("Item 4", 1, 4)
                    })
               }.AsReadOnly()
           };
        yield return new object[]
            {
                new List<SelectGroup<int>>
                {
                 new SelectGroup<int>("Group 1", 0,
                    new List<SelectItem<int>>
                    {
                        new SelectItem<int>("Item 1", 0, 1),
                        new SelectItem<int>("Item 2", 1, 2)
                    }),
                 new SelectGroup<int>("Group 2", 1,
                    new List<SelectItem<int>>
                    {
                        new SelectItem<int>("Item 3", 0, 3),
                        new SelectItem<int>("Item 4", 1, 4)
                    }),
                  new SelectGroup<int>("Group 3", 2,
                    new List<SelectItem<int>>
                    {
                        new SelectItem<int>("Item 5", 0, 3)
                    })
                }.AsReadOnly()
           };
    }


    public static readonly IReadOnlyCollection<SelectItem<int>> SelectItems = new List<SelectItem<int>>()
    {
        new SelectItem<int>("Item 1", 0, 1),
        new SelectItem<int>("Item 2", 1, 2),
        new SelectItem<int>("Item 3", 2, 3)
    };

    public static readonly IReadOnlyCollection<SelectGroup<int>> Groups = new List<SelectGroup<int>>
    {
        new SelectGroup<int>("Group 1", 0,
            new List<SelectItem<int>>
            {
                new SelectItem<int>("Item 1", 0, 1),
                new SelectItem<int>("Item 2", 1, 2)
            }.AsReadOnly()
         ),
         new SelectGroup<int>("Group 2", 1,
            new List<SelectItem<int>>
            {
                new SelectItem<int>("Item 3", 0, 3),
                new SelectItem<int>("Item 4", 1, 4)
            }.AsReadOnly()
         )
    };
};



