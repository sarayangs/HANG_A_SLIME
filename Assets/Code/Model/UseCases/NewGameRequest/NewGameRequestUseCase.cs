using System.Net;
using System.Threading.Tasks;
using UniRx;

public class NewGameRequestUseCase : INewGameRequester
{
    private readonly RestClientAdapter _restClientAdapter;
    private readonly TokenRepository _tokenRepository;
    private readonly IEventDispatcherService _eventDispatcherService;

    public NewGameRequestUseCase(RestClientAdapter restClientAdapter, TokenRepository tokenRepository, IEventDispatcherService eventDispatcherService)
    {
        _restClientAdapter = restClientAdapter;
        _tokenRepository = tokenRepository;
        _eventDispatcherService = eventDispatcherService;
    }

    public async Task StartGame()
    {
        var request = new NewGameRequest();
        var response = await _restClientAdapter.Post<NewGameRequest, NewGameResponse>(EndPoints.NewGame, request);
        
        _tokenRepository.SetToken(response.token);
        _eventDispatcherService.Dispatch<HangmanData>(new HangmanData(response.hangman));
    }
}