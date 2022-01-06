public class GuessLetterUseCase : ILetterGuesser
{
    private readonly RestClientAdapter _restClientAdapter;
    private readonly TokenRepository _tokenRepository;
    private readonly IEventDispatcherService _eventDispatcherService;
    private readonly IHealthManager _healthManager;
    
    public GuessLetterUseCase(RestClientAdapter restClientAdapter, TokenRepository tokenRepository, IEventDispatcherService eventDispatcherService,
        IHealthManager healthManager)
    {
        _restClientAdapter = restClientAdapter;
        _tokenRepository = tokenRepository;
        _eventDispatcherService = eventDispatcherService;
        _healthManager = healthManager;
    }
    
    public async void GuessLetter(string letter)
    {
        var request = new GuessLetterRequest { letter = letter, token = _tokenRepository.GetToken() };
        var response = await
            _restClientAdapter
                .PutWithParametersOnUrl<GuessLetterRequest, GuessLetterResponse>
                (
                    EndPoints.GuessLetter,
                    request
                );
    
        _tokenRepository.SetToken(response.token);
        if(!response.correct)
            _healthManager.SubtractHealth();
        _eventDispatcherService.Dispatch<ResponseData>(new ResponseData(letter, response.hangman, response.correct));
    }
}