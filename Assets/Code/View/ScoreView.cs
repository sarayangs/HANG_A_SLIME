using System.Collections.Generic;
using System;
using UnityEngine;
using UniRx;
using DG.Tweening;

public class ScoreView : View
{
    [SerializeField] private ScoreItemView _scoreItemViewPrefab;
    [SerializeField] private RectTransform _scoreItemContainer;

    private List<ScoreItemView> _instantiatedScoreItems;
    
    private ScoreViewModel _viewModel;
    public void Setup(ScoreViewModel scoreViewModel)
    {
        _instantiatedScoreItems = new List<ScoreItemView>();

        _viewModel = scoreViewModel;
        _viewModel.IsVisible.Subscribe((isVisible) =>
            {
                if (isVisible)
                {
                    gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(transform.parent.gameObject.GetComponent<RectTransform>().rect.width, 0, 0);
                    gameObject.SetActive(isVisible);
                    gameObject.GetComponent<RectTransform>().DOLocalMoveX(0, 0.5f);
                }
                else
                {
                    gameObject.GetComponent<RectTransform>().DOLocalMoveX(-transform.parent.gameObject.GetComponent<RectTransform>().rect.width, 0.5f).OnComplete(() => { gameObject.SetActive(isVisible); });
                }
            }).AddTo(_disposables);

        _viewModel
            .Scores
            .ObserveAdd()
            .Subscribe(InstantiateScorePrefab)
            .AddTo(_disposables);
    }
    private void InstantiateScorePrefab(CollectionAddEvent<ScoreItemViewModel> scoreItemEntity)
    {
        var scoreItemView = Instantiate(_scoreItemViewPrefab, _scoreItemContainer);
        scoreItemView.Setup(scoreItemEntity.Value);

        _instantiatedScoreItems.Add(scoreItemView);
    }
}