public class LoginPanelController
{
        private readonly LoginPanelViewModel _viewModel;

        public LoginPanelController(LoginPanelViewModel viewModel)
        {
                _viewModel = viewModel;

                /*_viewModel.LoginButtonPressed.Subscribe(emailPass =>
                {
                        
                });*/
        }
}