using UnityEngine;

public class ScorePresenter
{
        private readonly ScoreViewModel _viewModel;

        public ScorePresenter(ScoreViewModel viewModel)
        {
                _viewModel = viewModel;

                var eventDispatcherService = ServiceLocator.Instance.GetService<IEventDispatcherService>();
                eventDispatcherService.Subscribe<RankingEntry>(DisplayScoreItem);
        }

        private void DisplayScoreItem(RankingEntry data)
        {
                var scoreItemViewModel = new ScoreItemViewModel(data.Position, data.Name, data.Score, data.Time);
                _viewModel.Scores.Add(scoreItemViewModel);
        }
}