using UniRx;

public class HomeController: Controller
{
    private readonly HomeViewModel _viewModel;
    private readonly ChangeNameViewModel _changeNameViewModel;
    
    private readonly ISceneHandler _changeSceneUseCase;
    private readonly IGetUserFromRepository _getUserFromRepositoryUseCase;
    private readonly IHealthManager _healthManager;

    public HomeController(HomeViewModel viewModel,ChangeNameViewModel changeNameViewModel, ISceneHandler changeSceneUsecase, IGetUserFromRepository getUserFromRepositoryUseCase,
        IHealthManager healthManager)
    {
        _viewModel = viewModel;
        _changeNameViewModel = changeNameViewModel;
        _changeSceneUseCase = changeSceneUsecase;
        _getUserFromRepositoryUseCase = getUserFromRepositoryUseCase;
        _healthManager = healthManager;

        _viewModel.PlayButtonPressed
            .Subscribe((_) =>
            {
                _healthManager.InitHealth();
                _changeSceneUseCase.PlayScene();
            }).AddTo(_disposables);

        _viewModel.ChangeNameButtonPressed.Subscribe((_) =>
        {
            _changeNameViewModel.IsVisible.Value = true;
        }).AddTo(_disposables);
        
        _getUserFromRepositoryUseCase.GetUserName();
    }
}
