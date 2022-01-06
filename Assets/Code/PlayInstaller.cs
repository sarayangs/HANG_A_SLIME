using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayInstaller : MonoBehaviour
{
    [SerializeField] private PlayView _playView;
    
    private readonly List<IDisposable> _disposables = new List<IDisposable>();

    private void Awake()
    {
        var playViewModel = new PlayViewModel();
        _playView.SetUp(playViewModel);


        var restClientAdapter = new RestClientAdapter();
        var tokenRepository = new TokenRepository();
        
        var userRepository = ServiceLocator.Instance.GetService<AccessUserData>();
        var eventDispatcherService = ServiceLocator.Instance.GetService<IEventDispatcherService>();

        //USE CASES---------------------------------------------------------
        var healthManager = new HealthManager(userRepository, eventDispatcherService);
        var guessLetterUseCase = new GuessLetterUseCase(restClientAdapter, tokenRepository, eventDispatcherService, healthManager);
        var newGameRequesterUseCase = new NewGameRequestUseCase(restClientAdapter, tokenRepository, eventDispatcherService, userRepository);
        var initGameUseCase = new InitGameUseCase(newGameRequesterUseCase);
        
        //PRESENTERS--------------------------------------------------------
        new PlayPresenter(playViewModel, eventDispatcherService);
        
        //CONTROLLERS------------------------------------------------------
        new PlayController(playViewModel, initGameUseCase, guessLetterUseCase);
    }
    
    private void OnDestroy()
    {
        foreach (var disposable in _disposables)
        {
            disposable.Dispose();
        }
    }
}