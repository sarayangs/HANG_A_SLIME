using UniRx;

public class SettingsPresenter : Presenter
{
    private readonly SettingsViewModel _viewModel;
    private readonly IEventDispatcherService _eventDispatcherService;
    
    public SettingsPresenter(SettingsViewModel viewModel, IEventDispatcherService eventDispatcherService)
    {
        _viewModel = viewModel;
        _eventDispatcherService = eventDispatcherService;
        
        _eventDispatcherService.Subscribe<UserEntity>(OnSettingsInit);
    }
    
    public override void Dispose()
    {
        base.Dispose();
        _eventDispatcherService.Unsubscribe<UserEntity>(OnSettingsInit);
    }
    
    private void OnSettingsInit(UserEntity user)
    {
        _viewModel.NotificationsOn.Value = user.Notifications;
        _viewModel.AudioOn.Value = user.Audio;
    }
}