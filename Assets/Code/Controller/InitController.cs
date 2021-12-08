public class InitController
{
    private readonly InitViewModel _viewModel;
    private readonly ILogin _loginUseCase;
    private readonly IChangeScene _changeSceneUseCase;
    
    public InitController(InitViewModel viewModel, ILogin loginUseCase, IChangeScene changeSceneUseCase)
    {
        _viewModel = viewModel;
        _loginUseCase = loginUseCase;
        _changeSceneUseCase = changeSceneUseCase;
        
        _loginUseCase.Login();
        //_changeSceneUseCase.ChangeSceneTo("Menu");
    }
}
