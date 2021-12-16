using UniRx;
using UnityEngine;

public class LoginPanelPresenter : Presenter
{
        private readonly LoginPanelViewModel _viewModel;
        private readonly SettingsViewModel _settingsViewModel;


        private readonly IEventDispatcherService _eventDispatcherService;
        public LoginPanelPresenter(LoginPanelViewModel viewModel, SettingsViewModel settingsViewModel, IEventDispatcherService eventDispatcherService)
        {
                _viewModel = viewModel;
                _settingsViewModel = settingsViewModel;
                _eventDispatcherService = eventDispatcherService;

                _eventDispatcherService.Subscribe<bool>(OnLoginButtonPressed);
        }
        
        public override void Dispose()
        {
                base.Dispose();
                _eventDispatcherService.Unsubscribe<bool>(OnLoginButtonPressed);
        }

        private void OnLoginButtonPressed(bool data)
        {
                _viewModel.IsVisible.Value = false;
                _settingsViewModel.OnUserLogged.Value = true;
        }
}