public class GetUserFromRepositoryUseCase : IGetUserFromRepository
{
    private readonly IAccessUserData _accessUserData;
    private readonly IEventDispatcherService _eventDispatcherService;

    public GetUserFromRepositoryUseCase(IAccessUserData accessUserData, IEventDispatcherService eventDispatcherService)
    {
        _accessUserData = accessUserData; //repository
        _eventDispatcherService = eventDispatcherService;
    }
    public void GetUser()
    {
        _eventDispatcherService.Dispatch<string>(_accessUserData.GetLocalUser().Name);
    }
}