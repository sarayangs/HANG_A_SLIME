public class MenuInitializer : IMenuInitializer
{
    private readonly IGetUserFromRepository _getUserFromRepositoryUseCase;

    public MenuInitializer(IGetUserFromRepository getUserFromRepositoryUseCase)
    {
        _getUserFromRepositoryUseCase = getUserFromRepositoryUseCase;

    }

    public void Init()
    {
        _getUserFromRepositoryUseCase.GetUserName();
        _getUserFromRepositoryUseCase.GetUserSettings();
        _getUserFromRepositoryUseCase.GetUserType();
    }
}