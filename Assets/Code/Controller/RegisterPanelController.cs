using UniRx;
using UnityEngine;

public class RegisterPanelController : Controller
{
    private readonly RegisterPanelViewModel _viewModel;
    private readonly IRegisterUser _registerUserUseCase;
    public RegisterPanelController(RegisterPanelViewModel viewModel, IRegisterUser registerUseCase)
    {
        _viewModel = viewModel;
        _registerUserUseCase = registerUseCase;

        _viewModel.RegisterButtonPressed.Subscribe((emailPass) =>
        {
            _registerUserUseCase.Register(emailPass);
        }).AddTo(_disposables);
    }
}