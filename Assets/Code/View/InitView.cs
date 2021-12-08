using UniRx;
using UnityEngine;
public class InitView : View
{
    private InitViewModel _viewModel;

    public void Setup(InitViewModel viewModel)
    {
        _viewModel = viewModel;

        _viewModel.HasToLogNewUser.Subscribe(hasToLogNewUser => 
        {
            if(hasToLogNewUser)
                _viewModel.LoginNewUser.Execute();
        });
        
        _viewModel.UserExists.Subscribe((_) => 
        {
            _viewModel.LoginExistingUser.Execute();
        });
    }
}