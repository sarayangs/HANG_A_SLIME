using UniRx;

public class SettingsViewModel 
{
    public readonly ReactiveProperty<bool> IsVisible;

    public SettingsViewModel()
    {
        IsVisible = new ReactiveProperty<bool>();
    }
}