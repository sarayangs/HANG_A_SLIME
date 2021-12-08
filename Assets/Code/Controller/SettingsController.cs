using UniRx;
using UnityEngine;

public class SettingsController : MonoBehaviour
{
    private SettingsViewModel _viewModel;
    private LoginPanelViewModel _loginPanelViewModel;

    public SettingsController(SettingsViewModel viewModel,LoginPanelViewModel loginPanelViewModel)
    {
        _viewModel = viewModel;
        _loginPanelViewModel = loginPanelViewModel;

        _viewModel.LoginButtonPressed.Subscribe((_) =>
        {
            _loginPanelViewModel.IsVisible.Value = true;
        });
    }
}
