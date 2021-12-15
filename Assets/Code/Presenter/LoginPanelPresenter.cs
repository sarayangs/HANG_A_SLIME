public class LoginPanelPresenter
{
        private readonly LoginPanelViewModel _viewModel;
        private readonly IEventDispatcherService _eventDispatcherService;
        public LoginPanelPresenter(LoginPanelViewModel viewModel,  IEventDispatcherService eventDispatcherService)
        {
                _viewModel = viewModel;
                _eventDispatcherService = eventDispatcherService;
                
                _eventDispatcherService.Subscribe<bool>((data) =>
                {
                        _viewModel.IsVisible.Value = data;
                });
                
        }
}