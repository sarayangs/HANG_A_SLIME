using UniRx;
using UnityEngine;

public class SettingsController 
{
    private SettingsViewModel _viewModel;
    private LoginPanelViewModel _loginPanelViewModel;
    private RegisterPanelViewModel _registerPanelViewModel;
    
    private readonly IAudioManager _audioManagerUseCase;
    private readonly IMessagingManager _messagingManagerUseCase;
    private readonly IGetUserFromRepository _getUserFromRepositoryUseCase;

    public SettingsController(SettingsViewModel viewModel,LoginPanelViewModel loginPanelViewModel, RegisterPanelViewModel registerPanelViewModel,
        IAudioManager audioManagerUseCase, IMessagingManager messagingManagerUseCase, IGetUserFromRepository getUserFromRepository)
    {
        _viewModel = viewModel;
        _loginPanelViewModel = loginPanelViewModel;
        _registerPanelViewModel = registerPanelViewModel;
        _audioManagerUseCase = audioManagerUseCase;
        _messagingManagerUseCase = messagingManagerUseCase;
        _getUserFromRepositoryUseCase = getUserFromRepository;

        _viewModel.LoginButtonPressed.Subscribe((_) =>
        {
            _loginPanelViewModel.IsVisible.Value = true;
        });
        
        _viewModel.RegisterButtonPressed.Subscribe((_) =>
        {
            _registerPanelViewModel.IsVisible.Value = true;
        });
        
        _viewModel.OnNotificationChange.Subscribe((notificationsOn) =>
        {
            if (notificationsOn)
                _messagingManagerUseCase.Activate();
            else
                _messagingManagerUseCase.Deactivate();
        });
        _viewModel.OnAudioChange.Subscribe((audioOn) =>
        {
            if (audioOn)
                _audioManagerUseCase.Activate();
            else
                _audioManagerUseCase.Deactivate();
        });
        
        _getUserFromRepositoryUseCase.GetUserSettings();
    }
}
