<div align="center">
   <img src="../../../images/CarltonLogo.png" alt="Image Alt Text" width="200" />
</div>

# Carlton.Core.LayoutServices

The `Carlton.Core.LayoutServices` package contains a collection of services for layout state manegment, such as toasts, modals, and viewport management.

![C#](https://img.shields.io/badge/language-C%23-blue)
![ASP.NET](https://img.shields.io/badge/ASP.NET-blue)
![Blazor](https://img.shields.io/badge/Blazor-blue)

## Key Features

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
dotnet add package Carlton.Core.LayoutServices
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
      ToastState.RaiseToast("New Toast", "Hello World!", ToastTypes.Success.ToString());
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
        ModalState.RaiseModal(ModalTypes.ConfirmationModal.ToString(),
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

### ViewState Components

```cshtml
@using Carlton.Core.Foundation.Web.ViewState
@inherits {Component}
@inject IViewStateService ViewState

<MyComponent
    State="ViewState.Get<MyComponentState>(ViewStateKey)"
    OnValueChange="HandleValueChange"/>

@code {
    private string ViewStateKey => nameof(MyComponent);

	protected override void OnInitialized()
	{
        ViewState.InitializeKey<MyComponentState>(ViewStateKey);
		base.OnInitialized();
	}

	protected void HandleValueChange(ValueChangedArgs args)
	{
		ViewState.Set<int>(MyComponentState, args.NewState);
	}
}

```

## Authors

Contributors names and contact info

Nicholas Galazzo  
nicholas.galazzo@gmail.com

Stephen Galazzo  
Stephen.Galazzo@gmail.com

