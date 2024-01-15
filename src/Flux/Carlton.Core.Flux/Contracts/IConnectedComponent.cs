using Microsoft.AspNetCore.Components;

namespace Carlton.Core.Flux.Contracts;

public interface IConnectedComponent<TViewModel> : IComponent
{
    TViewModel ViewModel { get; set; }
    EventCallback OnComponentEvent { get; init; }
}
