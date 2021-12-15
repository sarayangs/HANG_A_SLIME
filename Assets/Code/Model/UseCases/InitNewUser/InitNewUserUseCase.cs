using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;

public class InitNewUserUseCase : IUserInitializer
{
    private readonly IDatabaseService _databaseService;
    private readonly IAuthenticationService _authenticationService;
    private readonly IAccessUserData _accessUserData;
    

    public InitNewUserUseCase(IDatabaseService databaseService, IAuthenticationService authenticationService, IAccessUserData accessUserData)
    {
        _databaseService = databaseService;
        _authenticationService = authenticationService;
        _accessUserData = accessUserData;
    }
        
    public async Task Init()
    {
        var userId = _authenticationService.UserId;
        var randomName = new RandomName();
        
        var userEntity = new UserEntity(userId, randomName.Name, true, true);
        var user = new UserDto(userId, randomName.Name, userEntity.Notifications, userEntity.Audio);
        
        Debug.Log($"New user: {userId}");
        
        _accessUserData.SetLocalUser(userEntity);
        await _databaseService.Save(user, "users", userId);
    }
}