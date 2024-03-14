<h1 align="center">
   <img src="../../Components/Carlton.Core.Components/wwwroot/images/CarltonLogo.png" alt="Image Alt Text" width="200" />
</br>
    Project Carlton
</br>

# Carlton.Core.Flux

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

* Carlton.Core.Foundation.Web

## Getting Started

### Installing

```bash
dotnet add package Carlton.Core.Flux
```
## Usage

### Implementing Required Interfaces

2. IConnectedComponent/BaseConntectedComponent
2. IViewModelProjectionMapper
3. IFluxStateMutation


### Register Flux Service

```cs
   var state = new MyState();
   builder.Services.AddCarltonFlux(state, opt =>
   {
      opt.AddLocalStorage = true;
      opt.AddHttpInterception = true;
   }); 
```

### Connected Components

ViewModel
```cs
public class CustomerViewModel
{
				public Id int { get; set; }
				public string Name { get; set; }
				public string Email { get; set; }
}
```
Component
```cshtml
@inherits BaseConnectedComponent<CustomerViewModel>

<div>
				<span>Id: @ViewModel.Id</span>
				<span>Name: @ViewModel.Name</span>
				<span>Email: @ViewModel.Email</span>
</div>

@code{
   //Additional code
}
```




## Authors

Contributors names and contact info

Nicholas Galazzo  
nicholas.galazzo@gmail.com

Stephen Galazzo  
Stephen.Galazzo@gmail.com

