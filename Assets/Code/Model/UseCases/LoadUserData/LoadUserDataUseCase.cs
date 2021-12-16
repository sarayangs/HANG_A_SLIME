using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class LoadUserDataUseCase : ILoadUserData
{
    private readonly IUserInitializer _userInitializer;
    private readonly IDatabaseService _databaseService;
    private readonly IAuthenticationService _authService;
    
    private readonly IAccessUserData _userRepository;
    private readonly ILoggedUsersRepository _loggedUsers;


    public LoadUserDataUseCase(IUserInitializer userInitializer, IAccessUserData userRepository, 
        ILoggedUsersRepository loggedUsers, IDatabaseService databaseService, IAuthenticationService authService)
    {
        _userInitializer = userInitializer;
        _databaseService = databaseService;
        _authService = authService;
        
        _userRepository = userRepository;
        _loggedUsers = loggedUsers;

    }

    public async Task LoadUserData()
    {
        var userId = _authService.UserId;
        var existUser = await _databaseService.ExistKey("users", userId);

        if (existUser)
        {
            var userData = await _databaseService.Load<UserDto>("users", userId);

            IReadOnlyList<RegisteredUser> registeredUsers = _loggedUsers.GetAll();

            if (registeredUsers != null)
            {
                foreach (var user in registeredUsers)
                {
                    if (user.UserId == userId)
                    {
                        Debug.Log($"Registered user: {user.Email}");
                        
                        var registeredUser = new UserEntity(user.UserId, user.Name, userData.Notifications, userData.Audio);
                        _userRepository.SetLocalUser(registeredUser);
                        
                        return;
                    }
                }
            }

            Debug.Log($"User exists: {userId}");
            
            _userRepository.SetLocalUser(new UserEntity(userData.Id, userData.Name, userData.Notifications, userData.Audio));

            return;
        }

        await _userInitializer.Init();
    }
}