using System.ComponentModel;
using UniRx;

public class PlayController : Controller
{
    private readonly PlayViewModel _viewModel;
    private readonly PauseViewModel _pauseViewModel;
    
    private readonly ILetterGuesser _guessLetterUseCase;

    public PlayController(PlayViewModel viewModel, PauseViewModel pauseViewModel, ILetterGuesser guessLetterUseCase)
    {
        _viewModel = viewModel;
        _pauseViewModel = pauseViewModel;
        _guessLetterUseCase = guessLetterUseCase;
        
        _viewModel.OnPauseButtonPressed.Subscribe((_) =>
        {
            _pauseViewModel.IsVisible.Value = true;
        }).AddTo(_disposables);
        
        _viewModel.KeyPressed.Subscribe((letter) =>
        {
            _guessLetterUseCase.GuessLetter(letter);
        });
    }
}