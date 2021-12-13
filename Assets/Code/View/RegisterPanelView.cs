using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class RegisterPanelView : View
{
    [SerializeField] private Button _registerButton;
    [SerializeField] private TMP_InputField _email;
    [SerializeField] private TMP_InputField _password;

    private RegisterPanelViewModel _viewModel;

    public void Setup(RegisterPanelViewModel viewModel)
    {
        _viewModel = viewModel;
        _viewModel.IsVisible.Subscribe(isVisible =>
        {
            gameObject.SetActive(isVisible);
        }).AddTo(_disposables);
        
        _registerButton.onClick.AddListener(() =>
        {
            _viewModel.RegisterButtonPressed.Execute(new KeyValuePair<string, string>(_email.text, _password.text));
        });
    }
}