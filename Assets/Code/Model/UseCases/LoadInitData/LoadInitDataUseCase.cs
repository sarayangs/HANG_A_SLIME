public class LoadInitDataUseCase
{
        private readonly ISceneHandler _sceneHandler;
        private readonly IAuthenticator _authenticator;
        private readonly ILoadUserData _userDataLoader;

        public LoadInitDataUseCase(ISceneHandler sceneHandler, IAuthenticator authenticator, ILoadUserData userDataLoader)
        {
                _sceneHandler = sceneHandler;
                _authenticator = authenticator;
                _userDataLoader = userDataLoader;
        }

        public async void Init()
        {
                await _authenticator.Authenticate();
                await _userDataLoader.LoadUserData();
                await _sceneHandler.ChangeSceneTo("Menu");
        }
}