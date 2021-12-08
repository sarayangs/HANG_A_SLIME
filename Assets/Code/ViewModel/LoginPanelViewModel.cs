using UniRx;
using System.Collections.Generic;

public class LoginPanelViewModel
{
        public readonly ReactiveProperty<bool> IsVisible;
        public readonly ReactiveCommand<KeyValuePair<string, string>> LoginButtonPressed;

        public LoginPanelViewModel()
        {
                IsVisible = new ReactiveProperty<bool>();
                LoginButtonPressed = new ReactiveCommand<KeyValuePair<string, string>>();
        }
}