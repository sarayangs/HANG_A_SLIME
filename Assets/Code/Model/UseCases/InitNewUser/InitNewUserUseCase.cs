using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;

public class InitNewUserUseCase : IUserInitializer
{
    private readonly IDatabaseService _databaseService;
    private readonly IRealtimeDatabase _realtimeDatabase;
    private readonly IAuthenticationService _authenticationService;
    private readonly IAccessUserData _accessUserData;
    

    public InitNewUserUseCase(IDatabaseService databaseService, IRealtimeDatabase realtimeDatabase, IAuthenticationService authenticationService, IAccessUserData accessUserData)
    {
        _databaseService = databaseService;
        _realtimeDatabase = realtimeDatabase;
        _authenticationService = authenticationService;
        _accessUserData = accessUserData;
    }
        
    public async Task Init()
    {
        var userId = _authenticationService.UserId;
        var randomName = new RandomName();
        
        var userEntity = new UserEntity(userId, randomName.Name, true, true, 0);
        var user = new UserDto(userId, randomName.Name, userEntity.Notifications, userEntity.Audio);
        
        Debug.Log($"New user: {userId}");
        
        _accessUserData.SetLocalUser(userEntity);
        var entry = new ScoreEntry(userId, 0, randomName.Name);
        _realtimeDatabase.AddData(entry);

        await _databaseService.Save(user, "users", userId);

    }
}