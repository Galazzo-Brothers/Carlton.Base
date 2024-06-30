<div align="center">
	<img src="../../../../images/CarltonLogo.png" alt="Image Alt Text" width="200" />
</div>

# Carlton Flux Debug (Carlton.Core.Flux.Debug)

<div align="center">
	<img src="../../../../images/CarltonFluxDebugScreenshot.jpg" alt="Image Alt Text" width="700" height="400" />
</div>  
<br/>


The `Carlton.Core.Flux.Debug` repository houses a powerful framework designed to debug and monitor Carlton Flux aplications. This framework allows developers to check logs, trace mutation and viewmodel requests and modify the existing central flux state.

![C#](https://img.shields.io/badge/language-C%23-blue)
![ASP.NET](https://img.shields.io/badge/ASP.NET-blue)
![Blazor](https://img.shields.io/badge/Blazor-blue)

## Key Features

- **Event Logs**: View front-end event logs as they come in, including the scopes and details of individual log messages.

- **Trace Logs**: Logs automatically aggregated based on their Viewmodel/command mutation request with detailed request context also available.

- **State Viewer**: View the existing centralized flux state of the application as both a json console and table of event sourced mutations. Project the current state into all available application viewmodels.

- **State Modification**: Add additional state mutations to the existing state to modify the current application in real time.

## Dependencies

* Carlton.Core.Flux
* Carlton.Core.Flux.Debug.Models
* Carlton.Core.Layouts

## Getting Started

### Installing

```bash
dotnet add package Carlton.Core.Flux.Debug
```
## Usage

### Register Lab Service

```cs
services.AddCarltonFluxDebug<LabState>();
```

```cs
NavigationManager.NavigateTo($"/debug/logs")
```
## Authors

Contributors names and contact info

Nicholas Galazzo  
nicholas.galazzo@gmail.com

Stephen Galazzo  
Stephen.Galazzo@gmail.com

