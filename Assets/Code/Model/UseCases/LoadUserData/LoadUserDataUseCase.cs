using System.Threading.Tasks;
using UnityEngine;

public class LoadUserDataUseCase : ILoadUserData
{
    private readonly IUserInitializer _userInitializer;
    private readonly IAccessUserData _accesUserData;
    private readonly IDatabaseService _databaseService;
    private readonly IAuthenticationService _authService;

    public LoadUserDataUseCase(IUserInitializer userInitializer, IAccessUserData accessUserData, IDatabaseService databaseService, IAuthenticationService authService)
    {
        _userInitializer = userInitializer;
        _accesUserData = accessUserData;
        _databaseService = databaseService;
        _authService = authService;
    }

    public async Task LoadUserData()
    {
        var userId = _authService.UserId;
        var existUser = await _databaseService.ExistKey("users", userId);

        if (existUser)
        {
            Debug.Log($"user exists: {userId}");
            var userData = await _databaseService.Load<User>("users", userId);
            _accesUserData.SetLocalUser(new UserEntity(userData.Id, userData.Name));

            return;
        }

        await _userInitializer.Init();
    }
}