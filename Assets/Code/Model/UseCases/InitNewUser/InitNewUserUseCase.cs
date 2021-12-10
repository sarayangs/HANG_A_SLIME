using System.Threading.Tasks;
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
        var name = "Random";

        var userEntity = new UserEntity(userId, name);
        _accessUserData.SetLocalUser(userEntity);

        var user = new User(userId, name);
        Debug.Log($"New user: {userId}");
        await _databaseService.Save(user, "users", userId);
    }
}