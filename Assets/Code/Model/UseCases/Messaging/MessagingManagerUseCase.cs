public class MessagingManagerUseCase : IMessagingManager
{
    private readonly IDatabaseService _databaseService;
    private readonly IAccessUserData _accessUserData;
    private readonly IMessagingService _messagingService;
    
    public MessagingManagerUseCase(IDatabaseService databaseService, IAccessUserData accessUserData, IMessagingService messagingService)
    {
        _databaseService = databaseService;
        _accessUserData = accessUserData;
        _messagingService = messagingService;
    }
    public void Activate()
    {
        var user = _accessUserData.GetLocalUser();
        user.Notifications = true;
        _accessUserData.SetLocalUser(user);
        
        var databaseUser = new UserDto(user.UserId, user.Name, user.Notifications, user.Audio);
        databaseUser.Notifications = true;
        
        _messagingService.ActivateMessaging();
        _databaseService.Save(databaseUser, "users", user.UserId);
    }

    public void Deactivate()
    {
        var user = _accessUserData.GetLocalUser();
        user.Notifications = false;
        _accessUserData.SetLocalUser(user);

        var databaseUser = new UserDto(user.UserId, user.Name, user.Notifications, user.Audio);
        databaseUser.Notifications = false;
        
        _messagingService.DeactivateMessaging();
        _databaseService.Save(databaseUser, "users", user.UserId);
    }
}