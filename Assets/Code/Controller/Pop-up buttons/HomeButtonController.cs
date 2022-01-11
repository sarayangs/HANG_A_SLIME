using UniRx;
using UnityEngine;

public class HomeButtonController : Controller
{
    private readonly HomeButtonViewModel _viewModel;
    private readonly ISceneHandler _changeSceneUseCase;

    public HomeButtonController(HomeButtonViewModel viewModel, ISceneHandler changeSceneUseCase)
    {
        _viewModel = viewModel;
        _changeSceneUseCase = changeSceneUseCase;

        _viewModel.OnHomeButtonPressed.Subscribe(_ =>
        {
            Debug.Log("hi");
            _changeSceneUseCase.ChangeSceneTo("Menu");
        }).AddTo(_disposables);
    }
}