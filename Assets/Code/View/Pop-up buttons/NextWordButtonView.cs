using UnityEngine;
using UnityEngine.UI;

public class NextWordButtonView : View
{
    [SerializeField] private Button _nextWordButton;

    private NextWordButtonViewModel _viewModel;

    public void Setup(NextWordButtonViewModel viewModel)
    {
        _viewModel = viewModel;
        
        _nextWordButton.onClick.AddListener(() =>
        {
            _viewModel.OnNextWordButtonPressed.Execute();
        });
    }
}