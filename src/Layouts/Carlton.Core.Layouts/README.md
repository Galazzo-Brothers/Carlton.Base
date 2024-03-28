<div align="center">
   <img src="../../../images/CarltonLogo.png" alt="Image Alt Text" width="200" />
</div>

# Carlton.Core.Layouts

The `Carlton.Core.Layouts` package provides a collection of pre-designed layouts for building rich and interactive user interfaces in Blazor applications. These layouts streamline the development process and offer out-of-the-box features such as toasts, modals, and viewport management.

![C#](https://img.shields.io/badge/language-C%23-blue)
![ASP.NET](https://img.shields.io/badge/ASP.NET-blue)
![Blazor](https://img.shields.io/badge/Blazor-blue)

## Key Features

- Responsive Dashboard Panel Layout
- Responsive Tabbed Dashboard Panel Layout
- Extensible LayoutManager Component

## Dependencies

* Carlton.Core.Foundation.Web
* Carlton.Core.LayoutServices
* Carlton.Core.Components

## Getting Started

### Installing

```bash
dotnet add package Carlton.Core.Layouts
```
### Implementing Layouts

1. Crate a new razor file {YourLayoutName}.razor.
2. Copy the following contents into your new layout file.
3. Replace the SectionContent tags with your own layout content as show below
4. Register the layout services in the IoC container

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

## Authors

Contributors names and contact info

Nicholas Galazzo  
nicholas.galazzo@gmail.com

Stephen Galazzo  
Stephen.Galazzo@gmail.com

