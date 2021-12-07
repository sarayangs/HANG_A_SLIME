using UniRx;

public class HomeController
{
    private readonly HomeViewModel _viewModel;
    private readonly ChangeNameViewModel _changeNameViewModel;
    
    private readonly IChangeScene _changeSceneUseCase;
    private readonly IGetUserData _getUserDataUseCase;

    public HomeController(HomeViewModel viewModel,ChangeNameViewModel changeNameViewModel, IChangeScene changeSceneUsecase, IGetUserData getUserDataUseCase)
    {
        _viewModel = viewModel;
        _changeNameViewModel = changeNameViewModel;
        _changeSceneUseCase = changeSceneUsecase;
        _getUserDataUseCase = getUserDataUseCase;

        _viewModel.PlayButtonPressed
            .Subscribe((_) =>
            {
                _changeSceneUseCase.ChangeSceneTo("Play");
            });

        _viewModel.ChangeNameButtonPressed.Subscribe((_) =>
        {
            _changeNameViewModel.IsVisible.Value = true;
        });
        
        _getUserDataUseCase.GetUserData();
    }
}
