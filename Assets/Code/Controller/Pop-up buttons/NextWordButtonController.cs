using UniRx;

public class NextWordButtonController : Controller
{ 
    private readonly NextWordButtonViewModel _viewModel;
    private readonly ISceneHandler _changeSceneUseCase;

    public NextWordButtonController(NextWordButtonViewModel viewModel, ISceneHandler changeSceneUseCase)
    {
        _viewModel = viewModel;
        _changeSceneUseCase = changeSceneUseCase;

        _viewModel.OnNextWordButtonPressed.Subscribe(_ =>
        {
            _changeSceneUseCase.PlayScene();
        });

    }
}