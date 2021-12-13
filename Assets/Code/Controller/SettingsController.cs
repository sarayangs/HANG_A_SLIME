using UniRx;
using UnityEngine;

public class SettingsController 
{
    private SettingsViewModel _viewModel;
    private LoginPanelViewModel _loginPanelViewModel;
    private RegisterPanelViewModel _registerPanelViewModel;

    public SettingsController(SettingsViewModel viewModel,LoginPanelViewModel loginPanelViewModel, RegisterPanelViewModel registerPanelViewModel)
    {
        _viewModel = viewModel;
        _loginPanelViewModel = loginPanelViewModel;
        _registerPanelViewModel = registerPanelViewModel;

        _viewModel.LoginButtonPressed.Subscribe((_) =>
        {
            _loginPanelViewModel.IsVisible.Value = true;
        });
        
        _viewModel.RegisterButtonPressed.Subscribe((_) =>
        {
            _registerPanelViewModel.IsVisible.Value = true;
        });
    }
}
