namespace Carlton.Core.Flux.State;



public record ViewModelRemoteRefreshCommand<TViewModel>(TViewModel ViewModel);

public class ViewModelRemoteRefreshCommandValidator<TViewModel> : AbstractValidator<ViewModelRemoteRefreshCommand<TViewModel>>
{
    public ViewModelRemoteRefreshCommandValidator(IValidator<TViewModel> validator)
    {
        RuleFor(_ => _.ViewModel).NotNull().SetValidator(validator);
    }
}
