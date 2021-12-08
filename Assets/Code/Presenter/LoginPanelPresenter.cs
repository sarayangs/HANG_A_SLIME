public class LoginPanelPresenter
{
        private readonly LoginPanelViewModel _viewModel;

        public LoginPanelPresenter(LoginPanelViewModel viewModel)
        {
                _viewModel = viewModel;
                
                var eventDispatcher = ServiceLocator.Instance.GetService<IEventDispatcherService>();
                eventDispatcher.Subscribe<User>((data) =>
                {
                        _viewModel.IsVisible.Value = false;
                });
                
        }
}