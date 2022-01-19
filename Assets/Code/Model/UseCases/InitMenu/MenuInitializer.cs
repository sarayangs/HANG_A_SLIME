public class MenuInitializer : IMenuInitializer
{
    private readonly IGetUserFromRepository _getUserFromRepositoryUseCase;
    private readonly ISoundHandler _soundUseCase;

    public MenuInitializer(IGetUserFromRepository getUserFromRepositoryUseCase, ISoundHandler soundUseCase)
    {
        _getUserFromRepositoryUseCase = getUserFromRepositoryUseCase;
        _soundUseCase = soundUseCase;
    }

    public void Init()
    {
        _getUserFromRepositoryUseCase.GetUserName();
        _getUserFromRepositoryUseCase.GetUserSettings();
        _getUserFromRepositoryUseCase.GetUserType();
        _soundUseCase.PlayMusic("bgMusic");
    }
}