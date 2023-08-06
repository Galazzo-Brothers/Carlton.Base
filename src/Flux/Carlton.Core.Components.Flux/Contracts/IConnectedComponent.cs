using Microsoft.AspNetCore.Components;
namespace Carlton.Core.Components.Flux;

public interface IConnectedComponent<TViewModel>
{
    TViewModel ViewModel { get; set; }
    Func<Task<TViewModel>> GetViewModel { get; init; }
    EventCallback<object> OnComponentEvent { get; init; }
}
