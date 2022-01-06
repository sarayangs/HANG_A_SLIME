using System.Net;
using System.Threading.Tasks;
using UniRx;

public class NewGameRequestUseCase : INewGameRequester
{
    private readonly RestClientAdapter _restClientAdapter;
    private readonly TokenRepository _tokenRepository;
    private readonly IAccessUserData _userRepository;
    private readonly IEventDispatcherService _eventDispatcherService;

    public NewGameRequestUseCase(RestClientAdapter restClientAdapter, TokenRepository tokenRepository, IEventDispatcherService eventDispatcherService,
        IAccessUserData userRepository)
    {
        _restClientAdapter = restClientAdapter;
        _tokenRepository = tokenRepository;
        _userRepository = userRepository;
        _eventDispatcherService = eventDispatcherService;
    }

    public async Task StartGame()
    {
        var request = new NewGameRequest();
        var response = await _restClientAdapter.Post<NewGameRequest, NewGameResponse>(EndPoints.NewGame, request);
        
        _tokenRepository.SetToken(response.token);
        _eventDispatcherService.Dispatch<HangmanData>(new HangmanData(response.hangman));
    }

    public void SetUserData()
    {
        _eventDispatcherService.Dispatch<UserEntity>(_userRepository.GetLocalUser());  
    }
}