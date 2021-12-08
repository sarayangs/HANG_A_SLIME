using UnityEngine;
public class InitPresenter
{
    private readonly InitViewModel _viewModel;
    private IEventDispatcherService _eventDispatcherService;

    public InitPresenter(InitViewModel viewModel)
    {
        _viewModel = viewModel;

        _eventDispatcherService = ServiceLocator.Instance.GetService<IEventDispatcherService>();

        _eventDispatcherService.Subscribe<bool>(OnLoginNewUser);
        _eventDispatcherService.Subscribe<string>(OnExistingUser);
    }

    private void OnLoginNewUser(bool hasToLogNewUser)
    {
        _viewModel.HasToLogNewUser.Value = hasToLogNewUser;
        Unsubscribe();
    }

    private void OnExistingUser(string data)
    {
        _viewModel.UserExists.Value = true;
        Unsubscribe();
    }

    private void Unsubscribe()
    {
        _eventDispatcherService.Unsubscribe<bool>(OnLoginNewUser);
        _eventDispatcherService.Unsubscribe<string>(OnExistingUser);
    }
}