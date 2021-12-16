public class RegisterPanelPresenter : Presenter
{
    private readonly RegisterPanelViewModel _viewModel;
    private readonly IEventDispatcherService _eventDispatcherService;


    public RegisterPanelPresenter(RegisterPanelViewModel viewModel, IEventDispatcherService eventDispatcherService)
    {
        _viewModel = viewModel;
        _eventDispatcherService = eventDispatcherService;

        _eventDispatcherService.Subscribe<bool>(OnRegisterButtonPressed);
    }

    public override void Dispose()
    {
        base.Dispose();
        _eventDispatcherService.Unsubscribe<bool>(OnRegisterButtonPressed);
    }

    private void OnRegisterButtonPressed(bool data)
    {
        _viewModel.IsVisible.Value = false; 
    }
}