using System.ComponentModel;
using UniRx;

public class PlayController : Controller
{
    private readonly PlayViewModel _viewModel;
    private readonly PauseViewModel _pauseViewModel;
    private readonly ITimeManager _timeManagerUseCase;

    private readonly ILetterGuesser _guessLetterUseCase;
    private readonly ISoundHandler _soundUseCase;

    public PlayController(PlayViewModel viewModel, PauseViewModel pauseViewModel, ILetterGuesser guessLetterUseCase,
        ITimeManager timeManagerUseCase, ISoundHandler soundUseCase)
    {
        _viewModel = viewModel;
        _pauseViewModel = pauseViewModel;
        _guessLetterUseCase = guessLetterUseCase;
        _timeManagerUseCase = timeManagerUseCase;
        _soundUseCase = soundUseCase;

        _viewModel.OnPauseButtonPressed.Subscribe((_) =>
        {
            _pauseViewModel.IsVisible.Value = true;
            _timeManagerUseCase.StopTimer();
            _soundUseCase.Play("pause");
        }).AddTo(_disposables);

        _viewModel.KeyPressed.Subscribe((letter) =>
        {
            _guessLetterUseCase.GuessLetter(letter);
        });
    }
}