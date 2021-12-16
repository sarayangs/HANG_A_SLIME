using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class LoadUserDataUseCase : ILoadUserData
{
    private readonly IUserInitializer _userInitializer;
    private readonly IDatabaseService _databaseService;
    private readonly IAuthenticationService _authService;
    
    private readonly IAccessUserData _userRepository;
    private readonly IRegisteredUsersRepository _registeredUsers;


    public LoadUserDataUseCase(IUserInitializer userInitializer, IAccessUserData userRepository, 
        IRegisteredUsersRepository registeredUsers, IDatabaseService databaseService, IAuthenticationService authService)
    {
        _userInitializer = userInitializer;
        _databaseService = databaseService;
        _authService = authService;
        
        _userRepository = userRepository;
        _registeredUsers = registeredUsers;

    }

    public async Task LoadUserData()
    {
        var userId = _authService.UserId;
        var existUser = await _databaseService.ExistKey("users", userId);

        if (existUser)
        {
            var userData = await _databaseService.Load<UserDto>("users", userId);

            IReadOnlyList<RegisteredUser> registeredUsers = _registeredUsers.GetAll();

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

            Debug.Log($"Anonymous user exists: {userId}");
            
            _userRepository.SetLocalUser(new UserEntity(userData.Id, userData.Name, userData.Notifications, userData.Audio));

            return;
        }

        await _userInitializer.Init();
    }
}