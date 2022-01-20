using UnityEngine;

public class GetUserFromRepositoryUseCase : IGetUserFromRepository
{
    private readonly IAccessUserData _accessUserData;
    private readonly ILoggedUsersRepository _loggedUsersRepository;
    private readonly IEventDispatcherService _eventDispatcherService;
    private readonly ISoundHandler _soundHandlerUseCase;

    public GetUserFromRepositoryUseCase(IAccessUserData accessUserData, ILoggedUsersRepository loggedUsersRepository, IEventDispatcherService eventDispatcherService,
        ISoundHandler soundHandlerUseCase)
    {
        _accessUserData = accessUserData;
        _loggedUsersRepository = loggedUsersRepository;
        _eventDispatcherService = eventDispatcherService;
        _soundHandlerUseCase = soundHandlerUseCase;
    }

    public void GetUserName()
    {
        _eventDispatcherService.Dispatch<string>(_accessUserData.GetLocalUser().Name);
    }

    public void GetUserSettings()
    {
        var user = _accessUserData.GetLocalUser();
        _soundHandlerUseCase.ToggleAudio(user.Audio);
        _eventDispatcherService.Dispatch<UserEntity>(user);
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