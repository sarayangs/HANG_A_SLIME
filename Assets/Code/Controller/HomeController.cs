using UniRx;

public class HomeController
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
                _changeSceneUseCase.ChangeSceneTo("Play");
            });

        _viewModel.ChangeNameButtonPressed.Subscribe((_) =>
        {
            _changeNameViewModel.IsVisible.Value = true;
        });
        
        _getUserFromRepositoryUseCase.GetUserName();
    }
}
