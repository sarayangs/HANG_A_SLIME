using UniRx;

public class HomeController: Controller
{
    private readonly HomeViewModel _viewModel;
    private readonly ChangeNameViewModel _changeNameViewModel;
    
    private readonly ISceneHandler _changeSceneUseCase;

    public HomeController(HomeViewModel viewModel,ChangeNameViewModel changeNameViewModel, ISceneHandler changeSceneUsecase)
    {
        _viewModel = viewModel;
        _changeNameViewModel = changeNameViewModel;
        _changeSceneUseCase = changeSceneUsecase;

        _viewModel.PlayButtonPressed
            .Subscribe((_) =>
            {
                _changeSceneUseCase.PlayScene();
            }).AddTo(_disposables);

        _viewModel.ChangeNameButtonPressed.Subscribe((_) =>
        {
            _changeNameViewModel.IsVisible.Value = true;
        }).AddTo(_disposables);
        
    }
}
