using System.Runtime.InteropServices;
using System.Threading;
using UniRx;
using UnityEngine;

public class SettingsPresenter : Presenter
{
    private readonly SettingsViewModel _viewModel;

    private readonly IEventDispatcherService _eventDispatcherService;
    
    public SettingsPresenter(SettingsViewModel viewModel, IEventDispatcherService eventDispatcherService)
    {
        _viewModel = viewModel;
        _eventDispatcherService = eventDispatcherService;
        
        _eventDispatcherService.Subscribe<UserEntity>(OnSettingsInit);
        _eventDispatcherService.Subscribe<bool>(UserLogged);
    }

    public override void Dispose()
    {
        base.Dispose();
        _eventDispatcherService.Unsubscribe<UserEntity>(OnSettingsInit);
        _eventDispatcherService.Unsubscribe<bool>(UserLogged);
    }
    
    private void OnSettingsInit(UserEntity user)
    {
        _viewModel.NotificationsOn.Value = user.Notifications;
        _viewModel.AudioOn.Value = user.Audio;
    }

    private void UserLogged(bool isLogged)
    {
        _viewModel.OnUserLogged.Value = isLogged;
    }

}