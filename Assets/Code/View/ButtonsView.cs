using UnityEngine.UI;
using UnityEngine;
using UniRx;
using DG.Tweening;

public class ButtonsView : View
{
    [SerializeField] private Button _homeButton;
    [SerializeField] private Button _scoreButton;
    [SerializeField] private Button _settingsButton;

    private ButtonsViewModel _buttonsViewModel;

    public void Setup(ButtonsViewModel buttonsViewModel)
    {
        _buttonsViewModel = buttonsViewModel;

        _homeButton.onClick.AddListener(() =>
        {
            _buttonsViewModel.HomeButtonPressed.Execute();
        });

        _scoreButton.onClick.AddListener(() =>
        {
            _buttonsViewModel.ScoreButtonPressed.Execute();
        });

        _settingsButton.onClick.AddListener(() =>
        {
            _buttonsViewModel.SettingsButtonPressed.Execute();
        });
        
        _buttonsViewModel.HomeIsPressed.Subscribe((isPressed) =>
        {
           DOColorOnButtons(_homeButton, isPressed);
        }).AddTo(_disposables);
        
        _buttonsViewModel.ScoreIsPressed.Subscribe((isPressed) =>
        {
            DOColorOnButtons(_scoreButton, isPressed);
        }).AddTo(_disposables);
        
        _buttonsViewModel.SettingsIsPressed.Subscribe((isPressed) =>
        {
            DOColorOnButtons(_settingsButton, isPressed);
        }).AddTo(_disposables);
    }

    private void DOColorOnButtons(Button button, bool isPressed)
    {
        if(isPressed)
            button.GetComponent<Image>().DOColor(new Color(0.5f, 0.5f ,0.5f), 0.2f);
        else 
            button.GetComponent<Image>().DOColor(new Color(1, 1, 1), 0.2f);
    }
}