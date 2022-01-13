using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UniRx;


public class PauseView : View
{
    [SerializeField] private RectTransform _buttonsParent;
    [SerializeField] private Button _resumeButton;
    [SerializeField] private HomeButtonView _homeButtonPrefab;
    [SerializeField] private RetryButtonView _retryButtonPrefab;

    private PauseViewModel _viewModel;
    private HomeButtonViewModel _homeButtonViewModel;
    private RetryButtonViewModel _retryButtonViewModel;

    public void Setup(PauseViewModel viewModel, HomeButtonViewModel homeButtonViewModel,
        RetryButtonViewModel retryButtonViewModel)
    {
        _viewModel = viewModel; 
        _homeButtonViewModel = homeButtonViewModel;
        _retryButtonViewModel = retryButtonViewModel;

        _viewModel.IsVisible.Subscribe(isVisible =>
        {
            gameObject.SetActive(isVisible);
            if (isVisible)
            {
                var homeButtonView = Instantiate(_homeButtonPrefab, _buttonsParent);
                homeButtonView.Setup(_homeButtonViewModel);

                var retryButtonView = Instantiate(_retryButtonPrefab, _buttonsParent);
                retryButtonView.Setup(_retryButtonViewModel);
            }
        }).AddTo(_disposables);
        _resumeButton.onClick.AddListener(() => { _viewModel.OnResumeButtonPressed.Execute();});
    }
        
}