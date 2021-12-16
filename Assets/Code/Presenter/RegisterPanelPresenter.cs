using UnityEngine;

public class RegisterPanelPresenter : Presenter
{
    private readonly RegisterPanelViewModel _viewModel;
    private readonly SettingsViewModel _settingsViewModel;

    private readonly IEventDispatcherService _eventDispatcherService;


    public RegisterPanelPresenter(RegisterPanelViewModel viewModel, SettingsViewModel settingsViewModel, IEventDispatcherService eventDispatcherService)
    {
        _viewModel = viewModel;
        _settingsViewModel = settingsViewModel;
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
        _settingsViewModel.OnUserLogged.Value = true;
    }
}