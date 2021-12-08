using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class MenuInstaller : MonoBehaviour
{
    [SerializeField] private RectTransform _canvasParent;
    [SerializeField] private ButtonsView _buttonView;

    [SerializeField] private HomeView _homeView;
    [SerializeField] private ScoreView _scoreView;
    [SerializeField] private SettingsView _settingsView;
    [SerializeField] private ChangeNameView _changeNameView;


    private void Awake()
    {
        var buttonsViewModel = new ButtonsViewModel();
        _buttonView.Setup(buttonsViewModel);

        var homeViewModel = new HomeViewModel();
        _homeView.Setup(homeViewModel);

        var scoreViewModel = new ScoreViewModel();
        _scoreView.Setup(scoreViewModel);

        var settingsViewModel = new SettingsViewModel();
        _settingsView.Setup(settingsViewModel);

        var changeNameViewModel = new ChangeNameViewModel();
        _changeNameView.Setup(changeNameViewModel);
        
        var changeSceneUseCase = new ChangeSceneUseCase();
        var getUserDataUseCase = new GetUserDataUseCase();
        var udpateUserDataUseCase = new UpdateUserDataUseCase();
        var rankingManagerUseCase = new RankingManagerUseCase();

        new HomePresenter(homeViewModel);
        new ScorePresenter(scoreViewModel);
        new ChangeNamePresenter(changeNameViewModel);

        new ButtonsController(buttonsViewModel,homeViewModel,scoreViewModel,settingsViewModel, rankingManagerUseCase);
        new HomeController(homeViewModel, changeNameViewModel, changeSceneUseCase, getUserDataUseCase);
        new ScoreController(scoreViewModel);
        new ChangeNameController(changeNameViewModel, udpateUserDataUseCase);
    }
}