public class InitGameUseCase : IGameInitializer
{
    private readonly INewGameRequester _newGameRequesterUseCase;

    public InitGameUseCase(INewGameRequester newGameRequesterUseCase)
    {
        _newGameRequesterUseCase = newGameRequesterUseCase;
    }

    public async void Start()
    {
        await _newGameRequesterUseCase.StartGame();
    }
}