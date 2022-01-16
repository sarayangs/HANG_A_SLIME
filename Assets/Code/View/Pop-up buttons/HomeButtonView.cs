using UnityEngine;
using UnityEngine.UI;

public class HomeButtonView : View
{
    [SerializeField] private Button _homeButton;

    private HomeButtonViewModel _viewModel;

    public void Setup(HomeButtonViewModel viewModel)
    {
        _viewModel = viewModel;
        
        _homeButton.onClick.AddListener(() =>
        {
            _viewModel.OnHomeButtonPressed.Execute();
        });
    }
}