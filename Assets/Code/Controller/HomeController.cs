using UniRx;

public class HomeController: Controller
{
    private readonly HomeViewModel _viewModel;
    private readonly ChangeNameViewModel _changeNameViewModel;
    private readonly ISoundHandler _soundUseCase;
    private readonly ISceneHandler _changeSceneUseCase;

    public HomeController(HomeViewModel viewModel,ChangeNameViewModel changeNameViewModel, ISceneHandler changeSceneUsecase,
        ISoundHandler soundUseCase)
    {
        _viewModel = viewModel;
        _changeNameViewModel = changeNameViewModel;
        _changeSceneUseCase = changeSceneUsecase;
        _soundUseCase = soundUseCase;

        _viewModel.PlayButtonPressed
            .Subscribe((_) =>
            {
                _changeSceneUseCase.PlayScene();
                _soundUseCase.Play("button");
            }).AddTo(_disposables);

        _viewModel.ChangeNameButtonPressed.Subscribe((_) =>
        {
            _changeNameViewModel.IsVisible.Value = true;
            _soundUseCase.Play("select");
        }).AddTo(_disposables);
        
    }
}
