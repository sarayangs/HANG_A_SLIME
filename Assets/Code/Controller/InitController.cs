public class InitController
{
    private readonly InitViewModel _viewModel;
    private readonly ILogin _loginUseCase;
    
    public InitController(InitViewModel viewModel, ILogin loginUseCase)
    {
        _viewModel = viewModel;
        _loginUseCase = loginUseCase;
        
        _loginUseCase.Login();
    }
}
