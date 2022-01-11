using System;
using System.Collections.Generic;
using Firebase.Database;
using UnityEngine;

public class PlayInstaller : MonoBehaviour
{
    [SerializeField] private PlayView _playView;
    [SerializeField] private ResultPopupView _resultPopupView;
    [SerializeField] private HomeButtonView _homeButtonView;
    [SerializeField] private RetryButtonView _retryButtonView;
    [SerializeField] private NextWordButtonView _nextWordButtonView;
    
    private readonly List<IDisposable> _disposables = new List<IDisposable>();

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

        var restClientAdapter = new RestClientAdapter();
        var tokenRepository = new TokenRepository();
        
        var userRepository = ServiceLocator.Instance.GetService<AccessUserData>();
        var loggedUsersRepository = ServiceLocator.Instance.GetService<LoggedUsersRepository>();
        var eventDispatcherService = ServiceLocator.Instance.GetService<IEventDispatcherService>();
        var sceneHandlerService = ServiceLocator.Instance.GetService<UnitySceneHandler>();
        var firebaseAnalytics = ServiceLocator.Instance.GetService<FirebaseAnalyticsService>();
        var firebaseFirestore = ServiceLocator.Instance.GetService<FirebaseFirestoreService>();
        var firesbaseRealtime = ServiceLocator.Instance.GetService<FirebaseDatabaseService>();
        
        //USE CASES---------------------------------------------------------
        var healthManager = new HealthManager(userRepository, eventDispatcherService);
        var checkResponseUsecase = new CheckResponseUseCase(eventDispatcherService, healthManager);
        var guessLetterUseCase = new GuessLetterUseCase(restClientAdapter, tokenRepository, checkResponseUsecase);
        var newGameRequesterUseCase = new NewGameRequestUseCase(restClientAdapter, tokenRepository, eventDispatcherService, userRepository);
        var initGameUseCase = new InitGameUseCase(newGameRequesterUseCase);
        var scoreManagerUseCase = new ScoreManagerUseCase(userRepository);
        var changeSceneUseCase = new ChangeSceneUseCase(sceneHandlerService, firebaseAnalytics);
        var updateUserData = new UpdateUserDataUseCase(firebaseFirestore, firesbaseRealtime, eventDispatcherService,
            userRepository, loggedUsersRepository);
            
        //PRESENTERS--------------------------------------------------------
        new PlayPresenter(playViewModel, eventDispatcherService);
        new ResultPopupPresenter(resultPopupViewModel, eventDispatcherService);
        
        //CONTROLLERS------------------------------------------------------
        new PlayController(playViewModel, initGameUseCase, guessLetterUseCase);
        new HomeButtonController(homeButtonViewModel, changeSceneUseCase);
        new RetryButtonController(retryButtonViewModel, updateUserData, changeSceneUseCase);
        new NextWordButtonController(nextWordButtonViewModel, changeSceneUseCase);

    }
    
    private void OnDestroy()
    {
        foreach (var disposable in _disposables)
        {
            disposable.Dispose();
        }
    }
}