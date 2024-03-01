<h1 align="center">
   <img src="../../Components/Carlton.Core.Components/wwwroot/images/CarltonLogo.png" alt="Image Alt Text" width="200" />
</br>
    Project Carlton
</br>

# Carlton.Core.Components.Layouts

The `Carlton.Core.Components.Layouts` package extends the functionality of the `Carlton.Core.Components` library by providing a collection of pre-designed layouts for building rich and interactive user interfaces in Blazor applications. These layouts streamline the development process and offer out-of-the-box features such as toasts, modals, and viewport management.

![C#](https://img.shields.io/badge/language-C%23-blue)
![ASP.NET](https://img.shields.io/badge/ASP.NET-blue)
![Blazor](https://img.shields.io/badge/Blazor-blue)

## Key Features

- Responsive DashboardPanelLayout Template
- Extensible LayoutManager Component
- MediaQueryWrapper Component and Viewport State Service
- FullScreen State Management and Menu Component
- Theme State Management and Menu Component
- Toast State Management and LayoutToaster Component
- Modal State Management


## Dependencies

* Carlton.Core.Components
* Carlton.Core.Foundation.Web

## Getting Started

### Installing

```bash
dotnet add package Carlton.Core.Components.Layouts
```
### Implementing Layouts

1. Crate a new razor file {YourLayoutName}.razor.
2. Copy the following contents into your new layout file.
3. Replace the SectionContent tags with your own layout content as show below
4. Register the layout services in the IoC container

#### Dashboard Layout

Desktop
<table>
  <tr>
    <td>NavSideBar</td>
    <td>Header</td>
    <td>Header</td>
  </tr>
  <tr>
    <td>NavSideBar</td>
    <td>Main</td>
    <td>Footer</td>
  </tr>
  <tr>
    <td>NavSideBar</td>
    <td>Main</td>
    <td>Footer</td>
  </tr>
 </table>

 Mobile
  Mobile
<table>
  <tr>
    <td>Header</td>
  </tr>
  <tr>
    <td>Main</td>
  </tr>
  <tr>
    <td>Footer</td>
  </tr>
 </table>

```cshtml
@using Carlton.Core.Components.Layouts.DashboardLayouts.DashboardLayout
@namespace Carlton.Core.Lab.Layouts
@layout DashboardLayout

@Body

<SectionContent SectionId="DashboardLayout.Nav">
   <!-- Replace with your panel content -->
</SectionContent>

<SectionContent SectionId="DashboardLayout.HeaderPageTitle">
   <!-- Replace with your page title content -->
</SectionContent>

<SectionContent SectionId="DashboardLayout.HeaderActionContent">
   <!-- Replace with your header action content -->
</SectionContent>

<SectionContent SectionId="DashboardLayout.Logo">
      <!-- Replace with your logo content -->
</SectionContent>

<SectionContent SectionId="DashboardLayout.Footer">
   <!-- Replace with your footer content -->
</SectionContent>
```
#### Dashboard Panel Layout

Desktop
<table>
  <tr>
    <td>NavSideBar</td>
    <td>Header</td>
    <td>Header</td>
  </tr>
  <tr>
    <td>NavSideBar</td>
    <td>Main/Panel</td>
    <td>Footer</td>
  </tr>
  <tr>
    <td>NavSideBar</td>
    <td>Main/Panel</td>
    <td>Footer</td>
  </tr>
 </table>

Mobile
<table>
  <tr>
    <td>Header</td>
  </tr>
  <tr>
    <td>Main/Panel</td>
  </tr>
  <tr>
    <td>Footer</td>
  </tr>
 </table>

```cshtml
@using Carlton.Core.Components.Layouts.DashboardLayouts.PanelLayout
@namespace Carlton.Core.Lab.Layouts
@layout DashboardPanelLayout

@Body

<SectionContent SectionId="DashboardPanelLayout.Nav">
   <!-- Replace with your panel content -->
</SectionContent>

<SectionContent SectionId="DashboardPanelLayout.HeaderPageTitle">
   <!-- Replace with your page title content -->
</SectionContent>

<SectionContent SectionId="DashboardPanelLayout.HeaderActionContent">
   <!-- Replace with your header action content -->
</SectionContent>

<SectionContent SectionId="DashboardPanelLayout.Logo">
   <!-- Replace with your logo content -->
</SectionContent>

<SectionContent SectionId="DashboardPanelLayout.Footer">
   <!-- Replace with your footer content -->
</SectionContent>
```
#### Dashboard Tabed Panel Layout

Desktop
<table>
  <tr>
    <td>NavSideBar</td>
    <td>Header</td>
    <td>Header</td>
  </tr>
  <tr>
    <td>NavSideBar</td>
    <td>Main/Tab Panel</td>
    <td>Footer</td>
  </tr>
  <tr>
    <td>NavSideBar</td>
    <td>Main/Tab Panel</td>
    <td>Footer</td>
  </tr>
 </table>

 Mobile
<table>
  <tr>
    <td>Header</td>
  </tr>
  <tr>
    <td>Main/Tab Panel</td>
  </tr>
  <tr>
    <td>Footer</td>
  </tr>
 </table>
 
```cshtml
@using Carlton.Core.Components.Layouts.DashboardLayouts.PanelLayout
@namespace Carlton.Core.Lab.Layouts
@layout DashboardTabbedPanelLayout

@Body

<SectionContent SectionId="DashboardPanelLayout.Nav">
   <!-- Replace with your panel content -->
</SectionContent>

<SectionContent SectionId="DashboardPanelLayout.HeaderPageTitle">
   <!-- Replace with your page title content -->
</SectionContent>

<SectionContent SectionId="DashboardPanelLayout.HeaderActionContent">
   <!-- Replace with your header action content -->
</SectionContent>

<SectionContent SectionId="DashboardPanelLayout.Footer">
   <!-- Replace with your footer content -->
</SectionContent>

<PageTabs>
    <TabContent>
        <Tab DisplayText="{Your tab text}" Icon="{Your mobile tab icon}">
            <!-- Replace with your tab content -->
        </Tab>
        <Tab DisplayText="{Your tab text}" Icon="{Your mobile tab icon}">
            <!-- Replace with your tab content -->
         </Tab>
        <Tab DisplayText="{Your tab text}" Icon="{Your mobile tab icon}">
            <!-- Replace with your tab content -->
        </Tab>
    </TabContent>
</PageTabs>
```
### Register Layout Service

```cs
   builder.Services.AddCarltonLayout(opt =>
   {
      opt.ShowPanel = true;
      opt.IsFullScreen = true;
      opt.Theme = Themes.dark;
   }); 
```
## Usage

### Toggle Fullscreen mode

```cshtml
@inject IFullScreenState FullScreenState

@* your markup *@

@code{
   public void ToggleFullScreen()
   {
      FullScreenState.ToggleFullScreen();
   }
}
```
### Change Panal Visibility

```cshtml
@inject IPanelState PanelState

@* your markup *@

@code{
   public void PanelState()
   {
      PanelState.TogglePanelVisibility()
   }
}
```

### Change Theme 

```cshtml
@inject IThemeState ThemeState

@* your markup *@

@code{
   public void ThemeState()
   {
      ThemeState.ToggleTheme()
   }
}
```

### Get Current Viewport

```cshtml
@inject IViewportState ViewportState

@* your markup *@

@code{
   public void ViewportState()
   {
      var currentViewport = ViewportState.GetCurrentViewport()
   }
}
```
### Raise a Toast

```cshtml
@inject IToastState ToastState

@* your markup *@

@code{
   public void RaiseHelloWorldToast()
   {
      ToastState.RaiseToast("New Toast", "Hello World!", ToastTypes.Success);
   }
}
```

### Show a Modal

```cshtml
@inject IModalState ModalState

@* your markup *@

@code{
   private void ShowConfirmationModal()
   {
        ModalState.RaiseModal(ModalTypes.ConfirmationModal,
        new ModalViewModel
        {
            Prompt = "Are you sure",
            Message = "Are you sure you want to do this, it cannot be undone?",
            CloseModal = OnModalClose,
            DismissModal = OnModalDimiss
        });
    }

   private async Task OnModalClose(ModalCloseEventArgs args)
   {
      //handle modal close
   }

    private async Task OnModalDismiss()
   {
      //handle modal dismiss
   }
}
```

## Authors

Contributors names and contact info

Nicholas Galazzo  
nicholas.galazzo@gmail.com

Stephen Galazzo  
Stephen.Galazzo@gmail.com

