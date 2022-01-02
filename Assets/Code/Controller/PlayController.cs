using UniRx;

public class PlayController : Controller
{
    private readonly PlayViewModel _viewModel;
    private readonly IGameInitializer _initGameUseCase;
    private readonly ILetterGuesser _guessLetterUseCase;

    public PlayController(PlayViewModel viewModel, IGameInitializer initGameUseCase, ILetterGuesser guessLetterUseCase)
    {
        _viewModel = viewModel;
        _initGameUseCase = initGameUseCase;
        _guessLetterUseCase = guessLetterUseCase;
        
        _viewModel.KeyPressed.Subscribe((letter) =>
        {
            _guessLetterUseCase.GuessLetter(letter);
        });

        _initGameUseCase.Start();
    }
}