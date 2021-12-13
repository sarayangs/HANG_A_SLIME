using UnityEngine;

public class UpdateUserDataUseCase : IUpdateUserData
{
    private readonly IAuthenticationService _authService;
    private readonly IDatabaseService _databaseService;
    private readonly IEventDispatcherService _eventDispatcherService;
    private readonly IAccessUserData _accessUserData;

    public UpdateUserDataUseCase(IAuthenticationService authenticationService, IDatabaseService databaseService, IEventDispatcherService eventDispatcherService, IAccessUserData accessUserData)
    {
        _authService = authenticationService;
        _databaseService = databaseService;
        _eventDispatcherService = eventDispatcherService;
        _accessUserData = accessUserData;
    }
    
    public void UpdateName(string newName)
    {
        string userId = _authService.UserId;
        
        var newUser = new UserDto(userId, newName);
        var newUserEntity = new UserEntity(userId, newName);
        
        _databaseService.Save(newUser, "users", userId);
        _accessUserData.SetLocalUser(newUserEntity);
        
        _eventDispatcherService.Dispatch<string>(newUser.Name);
    }
}