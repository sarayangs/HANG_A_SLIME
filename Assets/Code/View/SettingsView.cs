using UnityEngine;
using UnityEngine.UI;
using UniRx;
using DG.Tweening;

public class SettingsView : View
{
    [SerializeField] private Button _loginButton;
    [SerializeField] private Button _registerButton;
    [SerializeField] private Button _logoutButton;

    [SerializeField] private Toggle _notificationsToggle;
    [SerializeField] private Toggle _audioToggle;

    private SettingsViewModel _viewModel;
    public void Setup(SettingsViewModel settingsViewModel)
    {
        _viewModel = settingsViewModel;
        
        _viewModel.IsVisible.Subscribe((isVisible) =>
            {
                if (isVisible)
                {
                    gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(transform.parent.gameObject.GetComponent<RectTransform>().rect.width, 0, 0);
                    gameObject.SetActive(isVisible);
                    gameObject.GetComponent<RectTransform>().DOLocalMoveX(0, 0.3f);
                }
                else
                {
                    gameObject.GetComponent<RectTransform>().DOLocalMoveX(-transform.parent.gameObject.GetComponent<RectTransform>().rect.width, 0.3f).OnComplete(() => { gameObject.SetActive(isVisible); });
                }
            }).AddTo(_disposables);

        _viewModel.OnUserLogged.Subscribe(isLogged =>
        {
            if (isLogged)
            {
                _loginButton.gameObject.SetActive(false);
                _registerButton.gameObject.SetActive(false);
                _logoutButton.gameObject.SetActive(true);
            }
            else
            {
                _loginButton.gameObject.SetActive(true);
                _registerButton.gameObject.SetActive(true);
                _logoutButton.gameObject.SetActive(false);
            }

            
        }).AddTo(_disposables);

        _viewModel.NotificationsOn.Subscribe(isOn => { _notificationsToggle.isOn = isOn; });
        _viewModel.AudioOn.Subscribe(isOn => { _audioToggle.isOn = isOn; });

        _notificationsToggle.OnValueChangedAsObservable().Subscribe(isOn =>
        {
            _viewModel.OnNotificationChange.Execute(isOn);
        }).AddTo(_disposables);
        
        _audioToggle.OnValueChangedAsObservable().Subscribe(isOn =>
        {
            _viewModel.OnAudioChange.Execute(isOn);
        }).AddTo(_disposables);

        _loginButton.onClick.AddListener(() =>
        {
            _viewModel.LoginButtonPressed.Execute();
        });
        
        _registerButton.onClick.AddListener(() =>
        {
            _viewModel.RegisterButtonPressed.Execute();
        });
        
        _logoutButton.onClick.AddListener(() =>
        {
            _viewModel.LogoutButtonPressed.Execute();
        });
    }
}