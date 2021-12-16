using UniRx;

public class SettingsViewModel 
{
    public readonly ReactiveProperty<bool> IsVisible;
    public readonly ReactiveProperty<bool> NotificationsOn;
    public readonly ReactiveProperty<bool> AudioOn;
    public readonly ReactiveProperty<bool> OnUserLogged;
    
    public readonly ReactiveCommand LoginButtonPressed;
    public readonly ReactiveCommand RegisterButtonPressed;
    public readonly ReactiveCommand LogoutButtonPressed;
    public readonly ReactiveCommand<bool> OnNotificationChange;
    public readonly ReactiveCommand<bool> OnAudioChange;

    public SettingsViewModel()
    {
        IsVisible = new ReactiveProperty<bool>();
        NotificationsOn = new ReactiveProperty<bool>();
        AudioOn = new ReactiveProperty<bool>();
        OnUserLogged = new ReactiveProperty<bool>();

        LoginButtonPressed = new ReactiveCommand();
        RegisterButtonPressed = new ReactiveCommand();
        LogoutButtonPressed = new ReactiveCommand();
        OnNotificationChange = new ReactiveCommand<bool>();
        OnAudioChange = new ReactiveCommand<bool>();
    }
}