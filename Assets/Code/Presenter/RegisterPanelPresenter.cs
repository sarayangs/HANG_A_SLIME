public class RegisterPanelPresenter
{
    private readonly RegisterPanelViewModel _viewModel;

    public RegisterPanelPresenter(RegisterPanelViewModel viewModel)
    {
        _viewModel = viewModel;
                
        var eventDispatcher = ServiceLocator.Instance.GetService<IEventDispatcherService>();
        eventDispatcher.Subscribe<User>((data) =>
        {
            _viewModel.IsVisible.Value = false;
        });
                
    }
}