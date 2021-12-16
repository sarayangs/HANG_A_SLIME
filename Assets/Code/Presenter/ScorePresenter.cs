using UnityEngine;

public class ScorePresenter : Presenter
{
        private readonly ScoreViewModel _viewModel;
        private readonly IEventDispatcherService _eventDispatcherService;

        public ScorePresenter(ScoreViewModel viewModel,  IEventDispatcherService eventDispatcherService)
        {
                _viewModel = viewModel;
                _eventDispatcherService = eventDispatcherService;

                _eventDispatcherService.Subscribe<RankingEntry>(DisplayScoreItem);
        }
        public override void Dispose()
        {
                base.Dispose();
                _eventDispatcherService.Unsubscribe<RankingEntry>(DisplayScoreItem);
        }

        private void DisplayScoreItem(RankingEntry data)
        {
                var scoreItemViewModel = new ScoreItemViewModel(data.Position, data.Name, data.Score, data.Time);
                _viewModel.Scores.Add(scoreItemViewModel);
        }
}