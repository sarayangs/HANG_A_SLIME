using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Firebase.Auth;
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
    [SerializeField] private LoginPanelView _loginPanelView;
    [SerializeField] private RegisterPanelView _registerPanelView;


    private void Awake()
    {
        //VIEWMODELS AND VIEW SETUPS-------------------------------------------------------------
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

        var loginPanelViewModel = new LoginPanelViewModel();
        _loginPanelView.Setup(loginPanelViewModel);

        var registerPanelViewModel = new RegisterPanelViewModel();
        _registerPanelView.Setup(registerPanelViewModel);

        
        //GET SERVICES FROM SERVICE LOCATOR-------------------------------------------------------------
        var firebaseAuth = ServiceLocator.Instance.GetService<FirebaseAuthService>();
        var firebaseFirestore = ServiceLocator.Instance.GetService<FirebaseFirestoreService>();
        var sceneHandler = ServiceLocator.Instance.GetService<UnitySceneHandler>();
        var userRepository = ServiceLocator.Instance.GetService<UserRepository>();
        var eventDispatcherService = ServiceLocator.Instance.GetService<IEventDispatcherService>();
        
        //USE CASES-------------------------------------------------------------------------------------
        var changeSceneUseCase = new ChangeSceneUseCase(sceneHandler);
        var getUserFromRepositoryUseCase = new GetUserFromRepositoryUseCase(userRepository, eventDispatcherService);
        var udpateUserDataUseCase = new UpdateUserDataUseCase(firebaseAuth, firebaseFirestore, eventDispatcherService, userRepository);
        var rankingManagerUseCase = new RankingManagerUseCase();
        var registerUserUseCase = new RegisterUserUseCase(firebaseAuth, userRepository, eventDispatcherService);

        //PRESENTERS-------------------------------------------------------------------------------------
        new HomePresenter(homeViewModel);
        new ScorePresenter(scoreViewModel);
        new ChangeNamePresenter(changeNameViewModel);
        new LoginPanelPresenter(loginPanelViewModel);
        new RegisterPanelPresenter(registerPanelViewModel);

        //CONTROLLERS-------------------------------------------------------------------------------------
        new ButtonsController(buttonsViewModel,homeViewModel,scoreViewModel,settingsViewModel, rankingManagerUseCase);
        new HomeController(homeViewModel, changeNameViewModel, changeSceneUseCase, getUserFromRepositoryUseCase);
        new ScoreController(scoreViewModel);
        new SettingsController(settingsViewModel, loginPanelViewModel, registerPanelViewModel);
        new ChangeNameController(changeNameViewModel, udpateUserDataUseCase);
        new LoginPanelController(loginPanelViewModel);
        new RegisterPanelController(registerPanelViewModel, registerUserUseCase);
    }
}