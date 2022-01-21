using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class PlayInstaller : MonoBehaviour
{
    [SerializeField] private PlayView _playView;
    [SerializeField] private ResultPopupView _resultPopupView;
    [SerializeField] private HomeButtonView _homeButtonView;
    [SerializeField] private RetryButtonView _retryButtonView;
    [SerializeField] private NextWordButtonView _nextWordButtonView;
    [SerializeField] private PauseView _pauseView;
    
    private readonly List<IDisposable> _disposables = new List<IDisposable>();
    private IGameInitializer _initGameUseCase;

    private void Awake()
    {
        var playViewModel = new PlayViewModel();
        _playView.SetUp(playViewModel);
        
        var homeButtonViewModel = new HomeButtonViewModel();
        _homeButtonView.Setup(homeButtonViewModel);
        
        var retryButtonViewModel = new RetryButtonViewModel();
        _retryButtonView.Setup(retryButtonViewModel);
        
        var nextWordButtonViewModel = new NextWordButtonViewModel();
        _nextWordButtonView.Setup(nextWordButtonViewModel);
        
        var resultPopupViewModel = new ResultPopupViewModel();
        _resultPopupView.Setup(resultPopupViewModel, homeButtonViewModel, retryButtonViewModel, nextWordButtonViewModel);
        
        var pauseViewModel = new PauseViewModel();
        _pauseView.Setup(pauseViewModel, homeButtonViewModel, retryButtonViewModel);

        var restClientAdapter = new RestClientAdapter();
        var tokenRepository = new TokenRepository();
        
        var userRepository = ServiceLocator.Instance.GetService<AccessUserData>();
        var loggedUsersRepository = ServiceLocator.Instance.GetService<LoggedUsersRepository>();
        var eventDispatcherService = ServiceLocator.Instance.GetService<IEventDispatcherService>();
        var sceneHandlerService = ServiceLocator.Instance.GetService<UnitySceneHandler>();
        var firebaseAnalytics = ServiceLocator.Instance.GetService<FirebaseAnalyticsService>();
        var firebaseFirestore = ServiceLocator.Instance.GetService<FirebaseFirestoreService>();
        var firebaseRealtime = ServiceLocator.Instance.GetService<FirebaseDatabaseService>();
        var googleAdmob = ServiceLocator.Instance.GetService<GoogleAdmobService>();
        
        //USE CASES---------------------------------------------------------
        var soundHandlerUseCase = new SoundHandlerUseCase();
        var admobInitializer = new AdmobInitializer(googleAdmob, userRepository, firebaseAnalytics);
        var timeManagerUseCase = new TimeManagerUseCase(eventDispatcherService);
        var userStatsManagerUseCase = new UserStatsManagerUseCase(userRepository, eventDispatcherService, firebaseRealtime, 
            timeManagerUseCase, admobInitializer, soundHandlerUseCase);
        var healthManager = new HealthManager(userRepository, eventDispatcherService, userStatsManagerUseCase);
        var checkResponseUsecase = new CheckResponseUseCase(eventDispatcherService, healthManager, userStatsManagerUseCase, soundHandlerUseCase);
        var guessLetterUseCase = new GuessLetterUseCase(restClientAdapter, tokenRepository, checkResponseUsecase);
        var newGameRequesterUseCase = new NewGameRequestUseCase(restClientAdapter, tokenRepository, eventDispatcherService, userRepository);
        var changeSceneUseCase = new ChangeSceneUseCase(sceneHandlerService, firebaseAnalytics, userRepository);
        var updateUserData = new UpdateUserDataUseCase(firebaseFirestore, firebaseRealtime, eventDispatcherService,
            userRepository, loggedUsersRepository);
        
        _initGameUseCase = new InitGameUseCase(newGameRequesterUseCase, timeManagerUseCase, admobInitializer);

            
        //PRESENTERS--------------------------------------------------------
        new PlayPresenter(playViewModel, eventDispatcherService).AddTo(_disposables);
        new ResultPopupPresenter(resultPopupViewModel, eventDispatcherService).AddTo(_disposables);
        new PausePresenter(pauseViewModel, eventDispatcherService).AddTo(_disposables);
        
        //CONTROLLERS------------------------------------------------------
        new PlayController(playViewModel, pauseViewModel, guessLetterUseCase, timeManagerUseCase, soundHandlerUseCase)
            .AddTo(_disposables);
        new PauseController(pauseViewModel, timeManagerUseCase, soundHandlerUseCase).AddTo(_disposables);
        new HomeButtonController(homeButtonViewModel, updateUserData, changeSceneUseCase).AddTo(_disposables);
        new RetryButtonController(retryButtonViewModel, updateUserData, changeSceneUseCase).AddTo(_disposables);
        new NextWordButtonController(nextWordButtonViewModel, changeSceneUseCase).AddTo(_disposables);

    }

    private void Start()
    {
        _initGameUseCase.Start();
    }

    private void OnDestroy()
    {
        foreach (var disposable in _disposables)
        {
            disposable.Dispose();
        }
    }
}