using UniRx;
using UnityEngine;

public class RegisterPanelController : Controller
{
    private readonly RegisterPanelViewModel _viewModel;
    private readonly IRegisterUser _registerUserUseCase;
    private readonly ISoundHandler _soundUseCase;
    public RegisterPanelController(RegisterPanelViewModel viewModel, IRegisterUser registerUseCase,
        ISoundHandler soundUseCase)
    {
        _viewModel = viewModel;
        _registerUserUseCase = registerUseCase;
        _soundUseCase = soundUseCase;

        _viewModel.RegisterButtonPressed.Subscribe((emailPass) =>
        {
            _registerUserUseCase.Register(emailPass);
            _soundUseCase.Play("select");
        }).AddTo(_disposables);
    }
}