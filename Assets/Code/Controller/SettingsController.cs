using UniRx;
using UnityEngine;

public class SettingsController : Controller
{
    private SettingsViewModel _viewModel;
    private LoginPanelViewModel _loginPanelViewModel;
    private RegisterPanelViewModel _registerPanelViewModel;

    private readonly IAudioManager _audioManagerUseCase;
    private readonly IMessagingManager _messagingManagerUseCase;
    private readonly ILogoutUser _logoutUserUseCase;

    private readonly ISoundHandler _soundUseCase;

    public SettingsController(SettingsViewModel viewModel, LoginPanelViewModel loginPanelViewModel,
        RegisterPanelViewModel registerPanelViewModel,
        IAudioManager audioManagerUseCase, IMessagingManager messagingManagerUseCase, ILogoutUser logoutUserUseCase,
        ISoundHandler soundUseCase)
    {
        _viewModel = viewModel;
        _loginPanelViewModel = loginPanelViewModel;
        _registerPanelViewModel = registerPanelViewModel;
        _audioManagerUseCase = audioManagerUseCase;
        _messagingManagerUseCase = messagingManagerUseCase;
        _logoutUserUseCase = logoutUserUseCase;
        _soundUseCase = soundUseCase;

        _viewModel.LoginButtonPressed.Subscribe((_) =>
        {
            _loginPanelViewModel.IsVisible.Value = true;
            _soundUseCase.Play("select");
        }).AddTo(_disposables);

        _viewModel.RegisterButtonPressed.Subscribe((_) =>
        {
            _registerPanelViewModel.IsVisible.Value = true;
            _soundUseCase.Play("select");
        }).AddTo(_disposables);

        _viewModel.LogoutButtonPressed.Subscribe(_ =>
        {
            _logoutUserUseCase.Logout();
            _soundUseCase.Play("select");
        }).AddTo(_disposables);

        _viewModel.OnNotificationChange.Subscribe((notificationsOn) =>
        {
            if (notificationsOn)
                _messagingManagerUseCase.Activate();
            else
                _messagingManagerUseCase.Deactivate();

        }).AddTo(_disposables);

        _viewModel.OnAudioChange.Subscribe((audioOn) =>
        {
            if (audioOn)
                _audioManagerUseCase.Activate();
            else
                _audioManagerUseCase.Deactivate();
            
        }).AddTo(_disposables);
    }
}