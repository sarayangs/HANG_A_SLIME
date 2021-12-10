using UniRx;
using UnityEngine;

public class InitController
{
    private readonly InitViewModel _viewModel;
    private readonly IAuthenticator _loginUseCase;
    
    public InitController(InitViewModel viewModel, IAuthenticator loginUseCase, ISceneHandler changeSceneUseCase)
    {
        _viewModel = viewModel;
        _loginUseCase = loginUseCase;
        
        /*_loginUseCase.CheckExistingUser();

        _viewModel.LoginNewUser.Subscribe((_) =>
        {
            _loginUseCase.LoginNewUser();
        });
        
        _viewModel.LoginExistingUser.Subscribe((_) =>
        {
            _loginUseCase.LoginExistingUser();
        });*/
        
    }
}
