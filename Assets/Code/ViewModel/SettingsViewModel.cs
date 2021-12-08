using UniRx;

public class SettingsViewModel 
{
    public readonly ReactiveProperty<bool> IsVisible;
    public readonly ReactiveCommand LoginButtonPressed;
    public readonly ReactiveCommand RegisterButtonPressed;

    public SettingsViewModel()
    {
        IsVisible = new ReactiveProperty<bool>();
        LoginButtonPressed = new ReactiveCommand();
        RegisterButtonPressed = new ReactiveCommand();
    }
}