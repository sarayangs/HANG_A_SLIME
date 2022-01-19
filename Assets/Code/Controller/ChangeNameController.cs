using UniRx;
using UnityEngine;

public class ChangeNameController : Controller
{
    private readonly  ChangeNameViewModel _viewModel;
    private readonly IUpdateUserData _updateUserDataUseCase;
    private readonly ISoundHandler _soundUseCase;

    
    public ChangeNameController(ChangeNameViewModel viewModel, IUpdateUserData updateUserDataUseCase,
        ISoundHandler soundUseCase)
    {
        _viewModel = viewModel;
        _updateUserDataUseCase = updateUserDataUseCase;
        _soundUseCase = soundUseCase;

        _viewModel.SaveButtonPressed.Subscribe((name) =>
        {
            _updateUserDataUseCase.UpdateName(name);
            _soundUseCase.Play("select");
        }).AddTo(_disposables);
    }
}
