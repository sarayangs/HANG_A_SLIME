public class InitGameUseCase : IGameInitializer
{
    private readonly INewGameRequester _newGameRequesterUseCase;
    private readonly ITimeManager _timeManagerUseCase;
    private readonly IAdmobInitializer _admobInitializerUseCase;

    public InitGameUseCase(INewGameRequester newGameRequesterUseCase, ITimeManager timeManagerUseCase, IAdmobInitializer admobInitializerUseCase)
    {
        _newGameRequesterUseCase = newGameRequesterUseCase;
        _timeManagerUseCase = timeManagerUseCase;
        _admobInitializerUseCase = admobInitializerUseCase;
    }

    public async void Start()
    {
        await _newGameRequesterUseCase.StartGame();
        _admobInitializerUseCase.InitAd();
        _newGameRequesterUseCase.SetUserData();
        _timeManagerUseCase.StartTimer();
    }
}