using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class LoginPanelView : View
{
    [SerializeField] private Button _loginButton;
    [SerializeField] private TMP_InputField _email;
    [SerializeField] private TMP_InputField _password;

    private LoginPanelViewModel _viewModel;

    public void Setup(LoginPanelViewModel viewModel)
    {
        _viewModel = viewModel;
        _viewModel.IsVisible.Subscribe(isVisible =>
        {
            gameObject.SetActive(isVisible);
        }).AddTo(_disposables);
        
        _loginButton.onClick.AddListener(() =>
        {
            _viewModel.LoginButtonPressed.Execute(new KeyValuePair<string, string>(_email.text, _password.text));
        });
    }
}