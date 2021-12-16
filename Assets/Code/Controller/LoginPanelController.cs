﻿using UniRx;
public class LoginPanelController : Controller
{
        private readonly LoginPanelViewModel _viewModel;
        private readonly ISignInUser _signInUserUseCase;
        public LoginPanelController(LoginPanelViewModel viewModel, ISignInUser signInUserUseCase)
        {
                _viewModel = viewModel;
                _signInUserUseCase = signInUserUseCase;

                _viewModel.LoginButtonPressed.Subscribe(emailPass =>
                {
                        _signInUserUseCase.SignIn(emailPass);
                }).AddTo(_disposables);
        }
}