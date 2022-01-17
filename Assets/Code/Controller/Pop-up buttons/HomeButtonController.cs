using UniRx;
using UnityEngine;

public class HomeButtonController : Controller
{
    private readonly HomeButtonViewModel _viewModel;
    private readonly IUpdateUserData _updateUserDataUseCase;
    private readonly ISceneHandler _changeSceneUseCase;

    public HomeButtonController(HomeButtonViewModel viewModel,IUpdateUserData updateUserDataUseCase, ISceneHandler changeSceneUseCase)
    {
        _viewModel = viewModel;
        _changeSceneUseCase = changeSceneUseCase;
        _updateUserDataUseCase = updateUserDataUseCase;

        _viewModel.OnHomeButtonPressed.Subscribe(_ =>
        {
            _updateUserDataUseCase.ResetUser();
            _changeSceneUseCase.ChangeSceneTo("Menu");
        }).AddTo(_disposables);
    }
}