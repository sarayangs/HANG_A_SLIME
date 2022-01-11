public class CheckResponseUseCase : ICheckResponse
{
    private readonly IEventDispatcherService _eventDispatcherService;
    private readonly IHealthManager _healthManager;

    public CheckResponseUseCase(IEventDispatcherService eventDispatcherService,
        IHealthManager healthManager)
    {
        _eventDispatcherService = eventDispatcherService;
        _healthManager = healthManager;
    }
    public void CheckWord(GuessLetterResponse response, string letter)
    {
        if(!response.correct)
            _healthManager.SubtractHealth();
        
        if(IsCompleted(response.hangman))
            _eventDispatcherService.Dispatch<Answer>(new Answer(true));
        else
            _eventDispatcherService.Dispatch<ResponseData>(new ResponseData(letter, response.hangman, response.correct));
    }
    
    private bool IsCompleted(string hangman)
    {
        const string secretCharacter = "_";
        return !hangman.Contains(secretCharacter);
    }

}