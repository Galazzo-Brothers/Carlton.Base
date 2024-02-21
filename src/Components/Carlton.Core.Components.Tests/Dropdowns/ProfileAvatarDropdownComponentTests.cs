using Carlton.Core.Components.Dropdowns;
namespace Carlton.Core.Components.Tests.Dropdowns;


[Trait("Component", nameof(ProfileAvatarDropdown))]
public class ProfileAvatarDropdownComponentTests : TestContext
{
    [Theory(DisplayName = "Markup Test"), AutoData]
    public void ProfileAvatar_Markup_RendersCorrectly(
        string expectedAvatarUrl,
        string expectedUserName,
        IEnumerable<DropdownMenuItem<int>> expectedMenuItems)
    {
        //Arrange
        var expectedMarkup = BuildExpectedMarkup(expectedAvatarUrl, expectedUserName, expectedMenuItems);

        //Act
        var cut = RenderComponent<ProfileAvatarDropdown>(parameters => parameters
                    .Add(p => p.AvatarImgUrl, expectedAvatarUrl)
                    .Add(p => p.Username, expectedUserName)
                    .Add(p => p.DropdownMenuItems, expectedMenuItems));

        //Assert
        cut.MarkupMatches(expectedMarkup);
    }

    //TODO: Add Additiona Tests

    private static string BuildExpectedMarkup(string expectedAvatarUrl, string expectedUserName, IEnumerable<DropdownMenuItem<int>> menuItems)
    {
        var itemsMarkup = string.Join(Environment.NewLine, menuItems.Select(item => $@"
        <li>
          <span class=""mdi mdi-{item.MenuIcon} accent-color-{item.AccentColorIndex}""></span>
          <a href = ""#"">{item.MenuItemName}</a>
        </li>"));


        var markup = $@"
        <div class="" profile-avatar-dropdown"">
            <div class=""dropdown-menu"">
                <div class=""menu-template"">
                    <div class=""profile-avatar"">
                        <img class=""avatar-img text-center"" src=""{expectedAvatarUrl}"">
                        <span class=""username mobile-hide"">{expectedUserName}</span>
                        <span class=""icon mdi mdi-24px mdi-chevron-down mobile-hide""></span>
                    </div>
                </div>
                <div class=""dropdown-items"">
                  <div class=""header-template"">
                    <div class=""header"">
                      <span class=""user"">Stephen Galazzo</span>
                      <span class=""email"">Admin User</span>
                      <span class=""horizontal-spacer"" style="" --spacer-width:85%;--spacer-height:1px;""></span>
                    </div>
                  </div>
                  <ul> {itemsMarkup} </ul>
                </div>
              </div>
            </div>";

        return markup;
    }
}
