using UnityEngine;

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
    
    public override void Dispose()
    {
        base.Dispose();
        _eventDispatcherService.Unsubscribe<Answer>(OnFinishedWord);
    }


    private void OnFinishedWord(Answer data)
    {
        _viewModel.IsVisible.Value = true;

        if (data.Correct)
        {
            _viewModel.Win.Value = true;
            _viewModel.Text.Value = "YOU WIN!";
        }
        else
        {
            _viewModel.Lose.Value = true;
            _viewModel.Text.Value = "YOU LOSE...";
        }
        _viewModel.Score.Value = data.Score.ToString();
        _viewModel.Time.Value = data.Time.ToString();
    }
}