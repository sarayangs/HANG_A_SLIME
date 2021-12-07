using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInstaller : MonoBehaviour
{
    [SerializeField] private RectTransform _canvasParent;
    [SerializeField] private ButtonsView _buttonView;

    [SerializeField] private HomeView _homeView;
    [SerializeField] private ScoreView _scoreView;
    [SerializeField] private SettingsView _settingsView;


    private void Awake()
    {
        var buttonsViewModel = new ButtonsViewModel();
        var homeViewModel = new HomeViewModel();
        var scoreViewModel = new ScoreViewModel();
        var settingsViewModel = new SettingsViewModel();

        _buttonView.SetViewModel(buttonsViewModel);
        _homeView.SetViewModel(homeViewModel);
        _scoreView.SetViewModel(scoreViewModel);
        _settingsView.SetViewModel(settingsViewModel);

        new ButtonsController(buttonsViewModel,homeViewModel,scoreViewModel,settingsViewModel);
    }
}