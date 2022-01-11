using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UniRx;

public class ResultPopupView : View
{
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _score;
    [SerializeField] private TextMeshProUGUI _time;
    [SerializeField] private RectTransform _buttonsParent;

    [SerializeField] private HomeButtonView _homeButtonPrefab;
    [SerializeField] private RetryButtonView _retryButtonPrefab;
    [SerializeField] private NextWordButtonView _nextWordButtonPrefab;
    [SerializeField] private Sprite _sadImage;
    [SerializeField] private Sprite _happyImage;

    private ResultPopupViewModel _viewModel;

    private HomeButtonViewModel _homeButtonViewModel;
    private RetryButtonViewModel _retryButtonViewModel;
    private NextWordButtonViewModel _nextWordButtonViewModel;

    public void Setup(ResultPopupViewModel resultViewModel, HomeButtonViewModel homeButtonViewModel, RetryButtonViewModel retryButtonViewModel,
        NextWordButtonViewModel nextWordButtonViewModel)
    {
        _viewModel = resultViewModel;
        _homeButtonViewModel = homeButtonViewModel;
        _retryButtonViewModel = retryButtonViewModel;
        _nextWordButtonViewModel = nextWordButtonViewModel;
        
        _viewModel.Win.Subscribe(hasWon =>
        {
            var homeButtonView = Instantiate(_homeButtonPrefab, _buttonsParent);
            homeButtonView.Setup(_homeButtonViewModel);

            if (hasWon)
            {
                _image.sprite = _happyImage;

                var nextButtonView = Instantiate(_nextWordButtonPrefab, _buttonsParent);
                nextButtonView.Setup(_nextWordButtonViewModel);
            }
            else
            {
                _image.sprite = _sadImage;

                var retryButtonView = Instantiate(_retryButtonPrefab, _buttonsParent);
                retryButtonView.Setup(_retryButtonViewModel);
            }
        }).AddTo(_disposables);

        _viewModel.IsVisible.Subscribe(isVisible => { gameObject.SetActive(isVisible); }).AddTo(_disposables);
    }
}