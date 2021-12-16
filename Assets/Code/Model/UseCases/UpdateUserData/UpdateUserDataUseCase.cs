using UnityEngine;

public class UpdateUserDataUseCase : IUpdateUserData
{
    private readonly IDatabaseService _databaseService;
    private readonly IEventDispatcherService _eventDispatcherService;
    private readonly IAccessUserData _accessUserData;
    private readonly ILoggedUsersRepository _loggedUsersRepository;

    public UpdateUserDataUseCase(IDatabaseService databaseService, IEventDispatcherService eventDispatcherService, IAccessUserData accessUserData,
        ILoggedUsersRepository loggedUsersRepository)
    {
        _databaseService = databaseService;
        _eventDispatcherService = eventDispatcherService;
        _accessUserData = accessUserData;
        _loggedUsersRepository = loggedUsersRepository;
    }
    
    public void UpdateName(string newName)
    {
        var user = _accessUserData.GetLocalUser();
        
        var newUser = new UserDto(user.UserId, newName, user.Notifications, user.Audio);
        var newUserEntity = new UserEntity(user.UserId, newName, user.Notifications, user.Audio);
        var registeredUser = new RegisteredUser(user.UserId, newName, string.Empty, string.Empty);
        
        _databaseService.Save(newUser, "users", user.UserId);
        _accessUserData.SetLocalUser(newUserEntity);
        _loggedUsersRepository.UpdateUser(registeredUser);
        
        _eventDispatcherService.Dispatch<string>(newUser.Name);
    }
}