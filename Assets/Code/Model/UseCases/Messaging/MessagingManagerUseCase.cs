public class MessagingManagerUseCase : IMessagingManager
{
    private readonly IDatabaseService _databaseService;
    private readonly IAccessUserData _accessUserData;

    public MessagingManagerUseCase(IDatabaseService databaseService, IAccessUserData accessUserData)
    {
        _databaseService = databaseService;
        _accessUserData = accessUserData;
    }
    public void Activate()
    {
        var user = _accessUserData.GetLocalUser();
        user.Notifications = true;
        _accessUserData.SetLocalUser(user);
        
        var databaseUser = new UserDto(user.UserId, user.Name, user.Notifications, user.Audio);
        databaseUser.Notifications = true;
        
        _databaseService.Save(databaseUser, "users", user.UserId);
    }

    public void Deactivate()
    {
        var user = _accessUserData.GetLocalUser();
        user.Notifications = false;
        _accessUserData.SetLocalUser(user);

        var databaseUser = new UserDto(user.UserId, user.Name, user.Notifications, user.Audio);
        databaseUser.Notifications = false;
        
        _databaseService.Save(databaseUser, "users", user.UserId);
    }
}