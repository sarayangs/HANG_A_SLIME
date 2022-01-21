using UniRx;

public class RetryButtonController : Controller
{
    private readonly RetryButtonViewModel _viewModel;
    private readonly IUpdateUserData _updateUserDataUseCase;
    private readonly ISceneHandler _changeSceneUseCase;

    public RetryButtonController(RetryButtonViewModel viewModel, IUpdateUserData updateUserDataUseCase, ISceneHandler changeSceneUseCase)
    {
        _viewModel = viewModel;
        _updateUserDataUseCase = updateUserDataUseCase;
        _changeSceneUseCase = changeSceneUseCase;

        _viewModel.OnRetryButtonPressed.Subscribe(_ =>
        {
            _updateUserDataUseCase.ResetUser();
            _changeSceneUseCase.RetryPlay();
        }).AddTo(_disposables);
    
    }
}