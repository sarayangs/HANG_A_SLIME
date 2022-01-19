using UniRx;
public class LoginPanelController : Controller
{
        private readonly LoginPanelViewModel _viewModel;
        private readonly ISignInUser _signInUserUseCase;
        private readonly ISoundHandler _soundUseCase;
        public LoginPanelController(LoginPanelViewModel viewModel, ISignInUser signInUserUseCase,
                ISoundHandler soundUseCase)
        {
                _viewModel = viewModel;
                _signInUserUseCase = signInUserUseCase;
                _soundUseCase = soundUseCase;

                _viewModel.LoginButtonPressed.Subscribe(emailPass =>
                {
                        _signInUserUseCase.SignIn(emailPass);
                        _soundUseCase.Play("select");
                }).AddTo(_disposables);
        }
}