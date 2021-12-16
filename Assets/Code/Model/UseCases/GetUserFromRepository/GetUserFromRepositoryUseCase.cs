public class GetUserFromRepositoryUseCase : IGetUserFromRepository
{
    private readonly IAccessUserData _accessUserData;
    private readonly IEventDispatcherService _eventDispatcherService;

    public GetUserFromRepositoryUseCase(IAccessUserData accessUserData, IEventDispatcherService eventDispatcherService)
    {
        _accessUserData = accessUserData;
        _eventDispatcherService = eventDispatcherService;
    }
    public void GetUserName()
    {
        _eventDispatcherService.Dispatch<string>(_accessUserData.GetLocalUser().Name);
    }

    public void GetUserSettings()
    {
        _eventDispatcherService.Dispatch<UserEntity>(_accessUserData.GetLocalUser());
    }
}