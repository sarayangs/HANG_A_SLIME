using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class LoadUserDataUseCase : ILoadUserData
{
    private readonly IUserInitializer _userInitializer;
    private readonly IAccessUserData _userRepository;
    private readonly IDatabaseService _databaseService;
    private readonly IAuthenticationService _authService;


    public LoadUserDataUseCase(IUserInitializer userInitializer, IAccessUserData userRepository, IDatabaseService databaseService, IAuthenticationService authService)
    {
        _userInitializer = userInitializer;
        _userRepository = userRepository;
        _databaseService = databaseService;
        _authService = authService;
    }

    public async Task LoadUserData()
    {
        var userId = _authService.UserId;
        var existUser = await _databaseService.ExistKey("users", userId);

        if (existUser)
        {
            IReadOnlyList<UserEntity> registeredUsers = _userRepository.GetAll();

            if (registeredUsers != null)
            {
                foreach (var user in registeredUsers)
                {
                    if (user.UserId == userId)
                    {
                        Debug.Log($"Registered user: {user.Email}");
                        _userRepository.SetLocalUser(user);
                        return;
                    }
                }
            }

            Debug.Log($"Anonymous user exists: {userId}");
            var userData = await _databaseService.Load<UserDto>("users", userId);
            _userRepository.SetLocalUser(new UserEntity(userData.Id, userData.Name));

            return;
        }

        await _userInitializer.Init();
    }
}