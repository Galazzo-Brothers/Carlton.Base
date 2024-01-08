using Microsoft.AspNetCore.Components;

namespace Carlton.Core.Flux.Contracts;

public interface IConnectedComponent<TViewModel> : IComponent
{
    TViewModel ViewModel { get; set; }
    Func<Task<TViewModel>> GetViewModel { get; init; }
    EventCallback OnComponentEvent { get; init; }
}
