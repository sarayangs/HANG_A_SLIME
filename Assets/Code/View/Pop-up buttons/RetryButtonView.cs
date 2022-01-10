using UnityEngine;
using UnityEngine.UI;

public class RetryButtonView : View
{
    [SerializeField] private Button _retryButton;

    private RetryButtonViewModel _viewModel;

    public void Setup(RetryButtonViewModel viewModel)
    {
        _viewModel = viewModel;
        
        _retryButton.onClick.AddListener(() =>
        {
            _viewModel.OnRetryButtonPressed.Execute();
        });
    } 
}