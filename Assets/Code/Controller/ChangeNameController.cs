using UniRx;
using UnityEngine;

public class ChangeNameController
{
    private readonly  ChangeNameViewModel _viewModel;
    private readonly IUpdateUserData _updateUserDataUseCase;
    
    public ChangeNameController(ChangeNameViewModel viewModel, IUpdateUserData updateUserDataUseCase)
    {
        _viewModel = viewModel;
        _updateUserDataUseCase = updateUserDataUseCase;

        _viewModel.SaveButtonPressed.Subscribe((name) =>
        {
            _updateUserDataUseCase.UpdateName(name);
        });
    }
}
