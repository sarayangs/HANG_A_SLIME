using System;
using System.Collections.Generic;
using UniRx;
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

    private MenuInitializer _menuInitializer;

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
        var soundHandlerUseCase = new SoundHandlerUseCase();
        var changeSceneUseCase = new ChangeSceneUseCase(sceneHandler, firebaseAnalytics, accessUserData);
        var getUserFromRepositoryUseCase =
            new GetUserFromRepositoryUseCase(accessUserData, loggedUsersRepository, eventDispatcherService, soundHandlerUseCase);
        var udpateUserDataUseCase = new UpdateUserDataUseCase(firebaseFirestore, firebaseDatabase,
            eventDispatcherService, accessUserData, loggedUsersRepository);
        var rankingManagerUseCase = new RankingManagerUseCase(firebaseDatabase, eventDispatcherService);
        var registerUserUseCase = new RegisterUserUseCase(firebaseAuth, accessUserData, eventDispatcherService,
            firebaseFirestore, loggedUsersRepository);
        var signInuserUseCase = new SignInUserUseCase(firebaseAuth, eventDispatcherService, accessUserData,
            loggedUsersRepository, firebaseFirestore);
        var audioManagerUseCase = new AudioManagerUseCase(firebaseFirestore, accessUserData);
        var messagingManagerUseCase = new MessagingManagerUseCase(firebaseFirestore, accessUserData, firebaseMessaging);
        var logoutUserUseCase = new LogoutUserUseCase(firebaseAuth, eventDispatcherService);

        _menuInitializer = new MenuInitializer(getUserFromRepositoryUseCase, soundHandlerUseCase);

        //PRESENTERS-------------------------------------------------------------------------------------
        new HomePresenter(homeViewModel, eventDispatcherService).AddTo(_disposables);
        new ScorePresenter(scoreViewModel, eventDispatcherService).AddTo(_disposables);
        new ChangeNamePresenter(changeNameViewModel, eventDispatcherService).AddTo(_disposables);
        new LoginPanelPresenter(loginPanelViewModel, settingsViewModel, eventDispatcherService).AddTo(_disposables);
        new RegisterPanelPresenter(registerPanelViewModel, settingsViewModel, eventDispatcherService).AddTo(_disposables);
        new SettingsPresenter(settingsViewModel, eventDispatcherService).AddTo(_disposables);

        //CONTROLLERS-------------------------------------------------------------------------------------
        new ButtonsController(buttonsViewModel, homeViewModel, scoreViewModel, settingsViewModel,
            rankingManagerUseCase, soundHandlerUseCase).AddTo(_disposables);
        new HomeController(homeViewModel, changeNameViewModel, changeSceneUseCase, soundHandlerUseCase).AddTo(_disposables);
        new ScoreController(scoreViewModel).AddTo(_disposables);
        new SettingsController(settingsViewModel, loginPanelViewModel, registerPanelViewModel, audioManagerUseCase,
            messagingManagerUseCase, logoutUserUseCase, soundHandlerUseCase).AddTo(_disposables);
        new ChangeNameController(changeNameViewModel, udpateUserDataUseCase, soundHandlerUseCase).AddTo(_disposables);
        new LoginPanelController(loginPanelViewModel, signInuserUseCase, soundHandlerUseCase).AddTo(_disposables);
        new RegisterPanelController(registerPanelViewModel, registerUserUseCase, soundHandlerUseCase).AddTo(_disposables);
    }

    private void Start()
    {
        _menuInitializer.Init();
    }

    private void OnDestroy()
    {
        foreach (var disposable in _disposables)
        {
            disposable.Dispose();
        }
    }
}