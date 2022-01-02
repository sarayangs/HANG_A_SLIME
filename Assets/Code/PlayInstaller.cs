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
        var eventDispatcherService = ServiceLocator.Instance.GetService<IEventDispatcherService>();

        //USE CASES---------------------------------------------------------
        var guessLetterUseCase = new GuessLetterUseCase(restClientAdapter, tokenRepository, eventDispatcherService);
        var newGameRequesterUseCase = new NewGameRequestUseCase(restClientAdapter, tokenRepository, eventDispatcherService);
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