public class LoadInitDataUseCase
{
        private readonly ISceneHandler _sceneHandler;
        private readonly IAuthenticator _authenticator;
        private readonly ILoadUserData _userDataLoader;
        private readonly IAdmobInitializer _admobInitializer;

        public LoadInitDataUseCase(ISceneHandler sceneHandler, IAuthenticator authenticator, ILoadUserData userDataLoader, IAdmobInitializer admobInitializer)
        {
                _sceneHandler = sceneHandler;
                _authenticator = authenticator;
                _userDataLoader = userDataLoader;
                _admobInitializer = admobInitializer;
        }

        public async void Init()
        {
                _admobInitializer.Start();
                await _authenticator.Authenticate();
                await _userDataLoader.LoadUserData();
                await _sceneHandler.ChangeSceneTo("Menu");
        }
}