using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;

public class InitNewUserUseCase : IUserInitializer
{
    private readonly IDatabaseService _databaseService; //firestore
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
        
        var userEntity = new UserEntity(userId, randomName.Name);
        _accessUserData.SetLocalUser(userEntity);

        var user = new User(userId, randomName.Name);
        Debug.Log($"New user: {userId}");
        await _databaseService.Save(user, "users", userId);
    }
}