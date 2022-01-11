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
    [SerializeField] private TextMeshProUGUI _text;
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

    public void Setup(ResultPopupViewModel resultViewModel, HomeButtonViewModel homeButtonViewModel,
        RetryButtonViewModel retryButtonViewModel,
        NextWordButtonViewModel nextWordButtonViewModel)
    {
        _viewModel = resultViewModel;
        _homeButtonViewModel = homeButtonViewModel;
        _retryButtonViewModel = retryButtonViewModel;
        _nextWordButtonViewModel = nextWordButtonViewModel;

        _viewModel.Win.Subscribe(hasWon =>
        {
            if (hasWon)
            {
                _image.sprite = _happyImage;
                _text.text = "YOU WIN!";

                var homeButtonView = Instantiate(_homeButtonPrefab, _buttonsParent);
                homeButtonView.Setup(_homeButtonViewModel);

                var nextButtonView = Instantiate(_nextWordButtonPrefab, _buttonsParent);
                nextButtonView.Setup(_nextWordButtonViewModel);
            }
        }).AddTo(_disposables);

        _viewModel.Lose.Subscribe(hasLose =>
        {
            if (hasLose)
            {
                _image.sprite = _sadImage;
                _text.text = "YOU LOST...";

                var homeButtonView = Instantiate(_homeButtonPrefab, _buttonsParent);
                homeButtonView.Setup(_homeButtonViewModel);

                var retryButtonView = Instantiate(_retryButtonPrefab, _buttonsParent);
                retryButtonView.Setup(_retryButtonViewModel);
            }
        }).AddTo(_disposables);
        
        _viewModel.IsVisible.Subscribe(isVisible => { gameObject.SetActive(isVisible); }).AddTo(_disposables);
    }
}