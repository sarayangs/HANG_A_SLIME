public class ResultPopupPresenter : Presenter
{
    private readonly ResultPopupViewModel _viewModel;
    private readonly IEventDispatcherService _eventDispatcherService;

    public ResultPopupPresenter(ResultPopupViewModel viewModel, IEventDispatcherService eventDispatcherService)
    {
        _viewModel = viewModel;
        _eventDispatcherService = eventDispatcherService;

        _eventDispatcherService.Subscribe<Answer>(OnFinishedWord);
    }

    private void OnFinishedWord(Answer data)
    {
        _viewModel.IsVisible.Value = true;
        if(data.Correct)
            _viewModel.Win.Value = true;
        else
            _viewModel.Lose.Value = true;
    }
}