using UnityEngine;

public class UpdateUserDataUseCase : IUpdateUserData
{
    private readonly IDatabaseService _databaseService;
    private readonly IRealtimeDatabase _realtimeDatabase;
    private readonly IEventDispatcherService _eventDispatcherService;
    private readonly IAccessUserData _accessUserData;
    private readonly ILoggedUsersRepository _loggedUsersRepository;

    public UpdateUserDataUseCase(IDatabaseService databaseService, IRealtimeDatabase realtimeDatabase, IEventDispatcherService eventDispatcherService, IAccessUserData accessUserData,
        ILoggedUsersRepository loggedUsersRepository)
    {
        _databaseService = databaseService;
        _realtimeDatabase = realtimeDatabase;
        _eventDispatcherService = eventDispatcherService;
        _accessUserData = accessUserData;
        _loggedUsersRepository = loggedUsersRepository;
    }
    
    public void UpdateName(string newName)
    {
        var user = _accessUserData.GetLocalUser();
        
        var newUser = new UserDto(user.UserId, newName, user.Notifications, user.Audio);
        var newUserEntity = new UserEntity(user.UserId, newName, user.Notifications, user.Audio, user.Score);
        var registeredUser = new RegisteredUser(user.UserId, newName, string.Empty, string.Empty);
        
        _databaseService.Save(newUser, "users", user.UserId);
        _accessUserData.SetLocalUser(newUserEntity);
        _loggedUsersRepository.UpdateUser(registeredUser);
        _realtimeDatabase.AddData(new ScoreEntry(newUser.Id, user.Score, newName));
        _eventDispatcherService.Dispatch<string>(newUser.Name);
    }

    public void ResetUser()
    {
        var user = _accessUserData.GetLocalUser();
        user.Health = 9;
        user.Score = 0;
        user.CorrectWords = 0;
        user.GotAnotherChance = true;
        _accessUserData.SetLocalUser(user);
    }

}