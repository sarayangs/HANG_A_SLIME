using UnityEngine;

public class UpdateUserDataUseCase : IUpdateUserData
{
    private readonly IDatabaseService _databaseService;
    private readonly IEventDispatcherService _eventDispatcherService;
    private readonly IAccessUserData _accessUserData;
    private readonly IRegisteredUsersRepository _registeredUsersRepository;

    public UpdateUserDataUseCase(IDatabaseService databaseService, IEventDispatcherService eventDispatcherService, IAccessUserData accessUserData,
        IRegisteredUsersRepository registeredUsersRepository)
    {
        _databaseService = databaseService;
        _eventDispatcherService = eventDispatcherService;
        _accessUserData = accessUserData;
        _registeredUsersRepository = registeredUsersRepository;
    }
    
    public void UpdateName(string newName)
    {
        var user = _accessUserData.GetLocalUser();
        
        var newUser = new UserDto(user.UserId, newName, user.Notifications, user.Audio);
        var newUserEntity = new UserEntity(user.UserId, newName, user.Notifications, user.Audio);
        var registeredUser = new RegisteredUser(user.UserId, newName, string.Empty, string.Empty);
        
        _databaseService.Save(newUser, "users", user.UserId);
        _accessUserData.SetLocalUser(newUserEntity);
        _registeredUsersRepository.UpdateUser(registeredUser);
        
        _eventDispatcherService.Dispatch<string>(newUser.Name);
    }
}