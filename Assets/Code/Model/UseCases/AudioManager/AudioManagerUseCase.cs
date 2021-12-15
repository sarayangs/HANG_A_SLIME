using UnityEngine;

public class AudioManagerUseCase : IAudioManager
{
    private readonly IDatabaseService _databaseService;
    private readonly IAccessUserData _accessUserData;

    public AudioManagerUseCase(IDatabaseService databaseService, IAccessUserData accessUserData)
    {
        _databaseService = databaseService;
        _accessUserData = accessUserData;
    }
    
    public void Activate()
    {
        var user = _accessUserData.GetLocalUser();
        user.Audio = true;
        _accessUserData.SetLocalUser(user);
        
        var databaseUser = new UserDto(user.UserId, user.Name, user.Notifications, user.Audio);
        databaseUser.Audio = true;
        
        _databaseService.Save(databaseUser, "users", user.UserId);
    }

    public void Deactivate()
    {
        var user = _accessUserData.GetLocalUser();
        user.Audio = false;
        _accessUserData.SetLocalUser(user);

        var databaseUser = new UserDto(user.UserId, user.Name, user.Notifications, false);
        
         _databaseService.Save(databaseUser, "users", user.UserId);
    }
}