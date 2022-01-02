
public class PlayPresenter : Presenter
{
    private readonly PlayViewModel _viewModel;
    private readonly IEventDispatcherService _eventDispatcherService;

    public PlayPresenter(PlayViewModel viewModel, IEventDispatcherService eventDispatcherService)
    {
        _viewModel = viewModel;
        _eventDispatcherService = eventDispatcherService;

        _eventDispatcherService.Subscribe<HangmanData>(OnInitGame);
        _eventDispatcherService.Subscribe<ResponseData>(OnKeyPressed);
    }
    public override void Dispose()
    {
        base.Dispose();
        _eventDispatcherService.Unsubscribe<HangmanData>(OnInitGame);
    }
    private void OnInitGame(HangmanData data)
    {
        _viewModel.HangmanText.Value = AddSpacesBetweenLetters(data.HangmanText);
    }

    private void OnKeyPressed(ResponseData response)
    {
        if (response.Correct)
            _viewModel.CorrectLetters.Value += " " + response.Letter;
        else
            _viewModel.IncorrectLetters.Value +=  " " + response.Letter;
        
        _viewModel.HangmanText.Value = AddSpacesBetweenLetters(response.Hangman);
    }

    private static string AddSpacesBetweenLetters(string word)
    {
        return string.Join(" ", word.ToCharArray());
    }
}
