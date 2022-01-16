public class InitGameUseCase : IGameInitializer
{
    private readonly INewGameRequester _newGameRequesterUseCase;
    private readonly ITimeManager _timeManagerUseCase;

    public InitGameUseCase(INewGameRequester newGameRequesterUseCase, ITimeManager timeManagerUseCase)
    {
        _newGameRequesterUseCase = newGameRequesterUseCase;
        _timeManagerUseCase = timeManagerUseCase;
    }

    public async void Start()
    {
        await _newGameRequesterUseCase.StartGame();
        _newGameRequesterUseCase.SetUserData();
        _timeManagerUseCase.StartTimer();
    }
}