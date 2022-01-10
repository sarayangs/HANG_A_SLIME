using System;
using System.Collections.Generic;
using UnityEngine;

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
    
    private readonly List<IDisposable> _disposables = new List<IDisposable>();

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
        var firebaseDatabase = ServiceLocator.Instance.GetService<FirebaseDatabaseService>();
        var firebaseMessaging = ServiceLocator.Instance.GetService<FirebaseMessagingService>();
        var firebaseAnalytics = ServiceLocator.Instance.GetService<FirebaseAnalyticsService>();
        var sceneHandler = ServiceLocator.Instance.GetService<UnitySceneHandler>();
        var accessUserData = ServiceLocator.Instance.GetService<AccessUserData>();
        var loggedUsersRepository = ServiceLocator.Instance.GetService<LoggedUsersRepository>();
        var eventDispatcherService = ServiceLocator.Instance.GetService<IEventDispatcherService>();

        //USE CASES-------------------------------------------------------------------------------------
        var changeSceneUseCase = new ChangeSceneUseCase(sceneHandler, firebaseAnalytics);
        var getUserFromRepositoryUseCase = new GetUserFromRepositoryUseCase(accessUserData, loggedUsersRepository, eventDispatcherService);
        var udpateUserDataUseCase = new UpdateUserDataUseCase(firebaseFirestore, firebaseDatabase, eventDispatcherService, accessUserData, loggedUsersRepository);
        var rankingManagerUseCase = new RankingManagerUseCase(firebaseDatabase);
        var registerUserUseCase = new RegisterUserUseCase(firebaseAuth, accessUserData, eventDispatcherService, firebaseFirestore, loggedUsersRepository);
        var signInuserUseCase = new SignInUserUseCase(firebaseAuth, eventDispatcherService, accessUserData,
            loggedUsersRepository, firebaseFirestore);
        var audioManagerUseCase = new AudioManagerUseCase(firebaseFirestore, accessUserData);
        var messagingManagerUseCase = new MessagingManagerUseCase(firebaseFirestore, accessUserData, firebaseMessaging);
        var logoutUserUseCase = new LogoutUserUseCase(firebaseAuth, eventDispatcherService);
        var healthManager = new HealthManager(accessUserData, eventDispatcherService);

        //PRESENTERS-------------------------------------------------------------------------------------
        new HomePresenter(homeViewModel, eventDispatcherService);
        new ScorePresenter(scoreViewModel, eventDispatcherService);
        new ChangeNamePresenter(changeNameViewModel, eventDispatcherService);
        new LoginPanelPresenter(loginPanelViewModel, settingsViewModel, eventDispatcherService);
        new RegisterPanelPresenter(registerPanelViewModel, settingsViewModel, eventDispatcherService);
        new SettingsPresenter(settingsViewModel, eventDispatcherService);

        //CONTROLLERS-------------------------------------------------------------------------------------
        new ButtonsController(buttonsViewModel,homeViewModel,scoreViewModel,settingsViewModel, rankingManagerUseCase);
        new HomeController(homeViewModel, changeNameViewModel, changeSceneUseCase, getUserFromRepositoryUseCase, healthManager);
        new ScoreController(scoreViewModel);
        new SettingsController(settingsViewModel, loginPanelViewModel, registerPanelViewModel, audioManagerUseCase, messagingManagerUseCase, 
            getUserFromRepositoryUseCase, logoutUserUseCase);
        new ChangeNameController(changeNameViewModel, udpateUserDataUseCase);
        new LoginPanelController(loginPanelViewModel, signInuserUseCase);
        new RegisterPanelController(registerPanelViewModel, registerUserUseCase);
    }
    private void OnDestroy()
    {
        foreach (var disposable in _disposables)
        {
            disposable.Dispose();
        }
    }
}