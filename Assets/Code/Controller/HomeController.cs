using UniRx;

public class HomeController: Controller
{
    private readonly HomeViewModel _viewModel;
    private readonly ChangeNameViewModel _changeNameViewModel;
    
    private readonly ISceneHandler _changeSceneUseCase;
    private readonly IGetUserFromRepository _getUserFromRepositoryUseCase;

    public HomeController(HomeViewModel viewModel,ChangeNameViewModel changeNameViewModel, ISceneHandler changeSceneUsecase, IGetUserFromRepository getUserFromRepositoryUseCase)
    {
        _viewModel = viewModel;
        _changeNameViewModel = changeNameViewModel;
        _changeSceneUseCase = changeSceneUsecase;
        _getUserFromRepositoryUseCase = getUserFromRepositoryUseCase;

        _viewModel.PlayButtonPressed
            .Subscribe((_) =>
            {
                _changeSceneUseCase.PlayScene();
            }).AddTo(_disposables);

        _viewModel.ChangeNameButtonPressed.Subscribe((_) =>
        {
            _changeNameViewModel.IsVisible.Value = true;
        }).AddTo(_disposables);
        
        _getUserFromRepositoryUseCase.GetUserName();
    }
}
