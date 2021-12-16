using UnityEngine;

public class GetUserFromRepositoryUseCase : IGetUserFromRepository
{
    private readonly IAccessUserData _accessUserData;
    private readonly ILoggedUsersRepository _loggedUsersRepository;
    private readonly IEventDispatcherService _eventDispatcherService;

    public GetUserFromRepositoryUseCase(IAccessUserData accessUserData, ILoggedUsersRepository loggedUsersRepository, IEventDispatcherService eventDispatcherService)
    {
        _accessUserData = accessUserData;
        _loggedUsersRepository = loggedUsersRepository;
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

    public void GetUserType()
    {
        var loggedUsers =  _loggedUsersRepository.GetAll();
        var user = _accessUserData.GetLocalUser();
        
        foreach (var loggedUser in loggedUsers)
        {
            if (user.UserId == loggedUser.UserId)
            {
                _eventDispatcherService.Dispatch<bool>(true);
            }
        }
    }

}