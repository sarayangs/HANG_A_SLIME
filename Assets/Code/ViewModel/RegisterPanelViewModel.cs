using UniRx;
using System.Collections.Generic;
public class RegisterPanelViewModel
{
    public readonly ReactiveProperty<bool> IsVisible;
    public readonly ReactiveCommand<KeyValuePair<string, string>> RegisterButtonPressed;

    public RegisterPanelViewModel()
    {
        IsVisible = new ReactiveProperty<bool>();
        RegisterButtonPressed = new ReactiveCommand<KeyValuePair<string, string>>();
    }     
}