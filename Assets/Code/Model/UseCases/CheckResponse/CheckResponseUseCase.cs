public class CheckResponseUseCase : ICheckResponse
{
    private readonly IEventDispatcherService _eventDispatcherService;
    private readonly IHealthManager _healthManager;
    private readonly IUserStatsManager _userStatsManagerUseCase;

    public CheckResponseUseCase(IEventDispatcherService eventDispatcherService,
        IHealthManager healthManager, IUserStatsManager userStatsManagerUseCase)
    {
        _eventDispatcherService = eventDispatcherService;
        _healthManager = healthManager;
        _userStatsManagerUseCase = userStatsManagerUseCase;
    }
    public void CheckWord(GuessLetterResponse response, string letter)
    {
        if(!response.correct)
            _healthManager.SubtractHealth();
        
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