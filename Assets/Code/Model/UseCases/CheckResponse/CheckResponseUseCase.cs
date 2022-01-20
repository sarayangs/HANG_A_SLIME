public class CheckResponseUseCase : ICheckResponse
{
    private readonly IEventDispatcherService _eventDispatcherService;
    private readonly IHealthManager _healthManager;
    private readonly IUserStatsManager _userStatsManagerUseCase;
    private readonly ISoundHandler _soundHandlerUseCase;

    public CheckResponseUseCase(IEventDispatcherService eventDispatcherService,
        IHealthManager healthManager, IUserStatsManager userStatsManagerUseCase, ISoundHandler soundHandlerUseCase)
    {
        _eventDispatcherService = eventDispatcherService;
        _healthManager = healthManager;
        _userStatsManagerUseCase = userStatsManagerUseCase;
        _soundHandlerUseCase = soundHandlerUseCase;

    }
    public void CheckWord(GuessLetterResponse response, string letter)
    {
        if (!response.correct)
        {
            _soundHandlerUseCase.Play("error");
            _healthManager.SubtractHealth();
        }
        else
        {
            _soundHandlerUseCase.Play("correct");
        }
        
        _eventDispatcherService.Dispatch<ResponseData>(new ResponseData(letter, response.hangman, response.correct));

        if(IsCompleted(response.hangman))
            _userStatsManagerUseCase.ManageUserStats(true);
    }
    
    private bool IsCompleted(string hangman)
    {
        const string secretCharacter = "_";
        return !hangman.Contains(secretCharacter);
    }

}